using D4ServerTracker;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace D4ServerRate
{
    public partial class Form1 : Form
    {
        // version
        public const string VERSION = "1.0.4";

        // device id
        private static string deviceId = FastHash.CalculateUUID();

        // D4 server infos
        private string connectedServerIP = null;
        private int rating = -1;
        private int nvotes = 0;
        private bool voted = false;
        private bool outdated = false;

        // D4 MAX DAYS OUTDATED
        private const int MAX_OUTDATED_DAYS = 2;

        // D4 server port
        private const int D4_PORT_1 = 6112;
        private const int D4_PORT_2 = 6113;
        private const int D4_PORT_3 = 6114;

        // timer
        private static Timer myTimer = new Timer();
        private int counter = 0;
        private const int FORCE_UPDATE = 20;

        // mutex
        private static Mutex mut = new Mutex();

        // firebase
        private static IFirebaseConfig FBconfig = new FirebaseConfig
        {
            BasePath = "https://d4servertracker-default-rtdb.europe-west1.firebasedatabase.app/",
            AuthSecret = "ji4wUCE6vjzV107u7LMlog2fAnKHL2WH6WqQZ6Kj"
        };

        private static IFirebaseClient FBclient;

        public Form1()
        {
            FBclient = new FirebaseClient(FBconfig);
            if (FBclient == null)
            {
                throw new Exception("Cant connect to Firebase");
            }

            InitializeComponent();

            // tooltips
            new ToolTip().SetToolTip(this.rateButtonBad, "bad: server lags everywhere even in town");
            new ToolTip().SetToolTip(this.rateButtonLag, "lag: but still playable");
            new ToolTip().SetToolTip(this.rateButtonGood, "good: random lag");
            new ToolTip().SetToolTip(this.rateButtonExcellent, "excellent: no lag!");

            // reset
            ResetServerStatus();
            SetUiDisconnected();

            // initialize serverloop background worker
            this.serverLoopWorker.DoWork += this.ServerLoopWorker_DoWork;
            this.serverLoopWorker.RunWorkerCompleted += this.ServerLoopWorker_RunWorkerCompleted;

            // initialize servervote background worker
            this.serverVoteWorker.DoWork += this.ServerVoteWorker_DoWork;
            this.serverVoteWorker.RunWorkerCompleted += this.ServerVoteWorker_RunWorkerCompleted;

            // Sets the timer interval to 5 seconds.            
            myTimer.Tick += new EventHandler(TimerEventProcessor);
            myTimer.Interval = 3000;
            myTimer.Start();
        }

        /**
         * Timer
         */
        #region Timer
        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            // stop timer
            myTimer.Stop();
            // inc counter
            counter++;
            // check server status
            this.serverLoopWorker.RunWorkerAsync();
        }
        #endregion

        /**
         * Server Staus Worker
         */
        #region Server Staus Worker
        private void ServerLoopWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // There was an error during the operation.                
                var response = MessageBox.Show($"An error occurred: {e.Error.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (response == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
            else
            {
                // The operation completed normally.
                setUi();

                // restart the timer
                myTimer.Enabled = true;
            }
        }

        private void ServerLoopWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            try
            {
                mut.WaitOne();

                // Start the time-consuming operation.
                CheckServerAsync(bw).Wait();
            }
            finally
            {
                mut.ReleaseMutex();
            }
        }

        private async Task CheckServerAsync(BackgroundWorker bw)
        {
            // reset current server IP
            string currentServerIP = null;

            // find D3 current connected server
            var ip = IPGlobalProperties.GetIPGlobalProperties();
            foreach (var tcp in ip.GetActiveTcpConnections())
            {
                if (tcp.RemoteEndPoint.Port == D4_PORT_1 ||
                    tcp.RemoteEndPoint.Port == D4_PORT_2 ||
                    tcp.RemoteEndPoint.Port == D4_PORT_3)
                {
                    currentServerIP = tcp.RemoteEndPoint.Address.MapToIPv4().ToString();
                    break;
                }
            }

            // check disconnected
            if (currentServerIP == null)
            {
                // reset server info
                connectedServerIP = currentServerIP;
                rating = -1;
                nvotes = 0;
                voted = false;
                outdated = false;

                // reset counter
                counter = 0;

                return;
            }

            // get server rating on new server, after voting or after a while
            if (!currentServerIP.Equals(connectedServerIP) || rating == -1 || counter > FORCE_UPDATE)
            {
                // reset counter
                counter = 0;

                // call server rating
                try
                {
                    FirebaseResponse response = await FBclient.GetAsync("server/" + EscapeIP(currentServerIP));
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception("FB response error: " + response.StatusCode);
                    }

                    Dictionary<string, Rate> server = response.ResultAs<Dictionary<string, Rate>>();

                    // preset server info
                    connectedServerIP = currentServerIP;
                    rating = 0;
                    nvotes = 0;
                    voted = false;
                    outdated = false;

                    if (server != null)
                    {
                        int tot = 0;
                        long lastTick = 0;

                        foreach (var k in server.Keys)
                        {
                            nvotes++;
                            tot += server[k].rate;

                            // update max tick
                            if (server[k].timestamp > lastTick) lastTick = server[k].timestamp;

                            // set voted
                            if (deviceId.Equals(k)) voted = true;
                        }

                        // set rate
                        rating = (int)tot / nvotes;

                        // set outdated
                        if ((DateTime.Now - new DateTime(lastTick)).Days > MAX_OUTDATED_DAYS) outdated = true;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("FB error: " + e.Message);
                }
            }
        }
        #endregion

        /**
         * Server Vote Worker
         */
        #region Server Staus Worker
        private void ServerVoteWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // There was an error during the operation.                
                var response = MessageBox.Show($"An error occurred: {e.Error.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (response == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
            else
            {
                // The operation completed normally.
                this.buttonVote.Enabled = true;
            }
        }

        private void ServerVoteWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            try
            {
                mut.WaitOne();

                // Start the time-consuming operation.
                VoteServerAsync(bw).Wait();
            }
            finally
            {
                mut.ReleaseMutex();
            }
        }

        private async Task VoteServerAsync(BackgroundWorker bw)
        {
            // check server ip & rating
            if (connectedServerIP != null && GetRate() > 0)
            {
                // call server rating
                Rate rate = new Rate()
                {
                    rate = GetRate(),
                    timestamp = DateTime.Now.Ticks
                };

                try
                {
                    SetResponse response = await FBclient.SetAsync("server/" + EscapeIP(connectedServerIP) + "/" + deviceId, rate);
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception("FB response error: " + response.StatusCode);
                    }

                    Rate result = response.ResultAs<Rate>();
                    if (result.timestamp == rate.timestamp)
                    {
                        rating = -1;
                        return;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("FB error: " + e.Message);
                }
            }
        }
        #endregion

        /**
         * Status & UI
         */
        #region Status & Ui
        private void ResetServerStatus()
        {
            this.connectedServerIP = null;
            this.rating = -1;
        }

        private void SetUiDisconnected()
        {
            this.currentserverip.Text = "Disconnected";
            this.serverrating.Text = "N.A.";
            this.serverrating.ForeColor = System.Drawing.Color.Black;
            this.rateButtonBad.Checked = false;
            this.rateButtonLag.Checked = false;
            this.rateButtonGood.Checked = false;
            this.rateButtonExcellent.Checked = false;
            this.buttonVote.Enabled = false;
            this.groupBoxRate.Enabled = false;
        }

        private void setUi()
        {
            switch (rating)
            {
                case 0:
                    {
                        this.currentserverip.Text = connectedServerIP;
                        this.serverrating.Text = $"({nvotes}) not reviewed";
                        this.serverrating.ForeColor = System.Drawing.Color.DarkGray;
                        this.groupBoxRate.Enabled = true;
                        break;
                    }
                case 1:
                    {
                        this.currentserverip.Text = connectedServerIP;
                        this.serverrating.Text = $"{GetVoted()}{IsOutdated()}({nvotes}) bad";
                        this.serverrating.ForeColor = System.Drawing.Color.Red;
                        this.groupBoxRate.Enabled = true;
                        break;
                    }
                case 2:
                    {
                        this.currentserverip.Text = connectedServerIP;
                        this.serverrating.Text = $"{GetVoted()}{IsOutdated()}({nvotes}) laggy";
                        this.serverrating.ForeColor = System.Drawing.Color.DarkOrange;
                        this.groupBoxRate.Enabled = true;
                        break;
                    }
                case 3:
                    {
                        this.currentserverip.Text = connectedServerIP;
                        this.serverrating.Text = $"{GetVoted()}{IsOutdated()}({nvotes}) good";
                        this.serverrating.ForeColor = System.Drawing.Color.Green;
                        this.groupBoxRate.Enabled = true;
                        break;
                    }
                case 4:
                    {
                        this.currentserverip.Text = connectedServerIP;
                        this.serverrating.Text = $"{GetVoted()}{IsOutdated()}({nvotes}) excellent";
                        this.serverrating.ForeColor = System.Drawing.Color.Purple;
                        this.groupBoxRate.Enabled = true;
                        break;
                    }
                default:
                    {
                        SetUiDisconnected();
                        break;
                    }
            }
        }

        private int GetRate()
        {
            if (this.rateButtonBad.Checked) return 1;
            if (this.rateButtonLag.Checked) return 2;
            if (this.rateButtonGood.Checked) return 3;
            if (this.rateButtonExcellent.Checked) return 4;
            return 0;
        }

        private string GetVoted()
        {
            return voted ? Char.ConvertFromUtf32(0x2606) + " " : "";
        }

        private string IsOutdated()
        {
            return outdated ? Char.ConvertFromUtf32(0x1F547) + " " : "";
        }

        private void rateButtonBad_CheckedChanged(object sender, EventArgs e)
        {
            this.buttonVote.Enabled = true;
        }

        private void rateButtonLag_CheckedChanged(object sender, EventArgs e)
        {
            this.buttonVote.Enabled = true;
        }

        private void rateButtonGood_CheckedChanged(object sender, EventArgs e)
        {
            this.buttonVote.Enabled = true;
        }

        private void rateButtonExcellent_CheckedChanged(object sender, EventArgs e)
        {
            this.buttonVote.Enabled = true;
        }

        private void buttonVote_Click(object sender, EventArgs e)
        {
            this.buttonVote.Enabled = false;
            this.serverVoteWorker.RunWorkerAsync();
        }
        #endregion

        /**
         * Utility
         */
        private string EscapeIP(String ip)
        {
            return ip.Replace(".", "_");
        }
    }
}