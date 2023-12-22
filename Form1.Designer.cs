namespace D4ServerRate
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.currentserverip = new System.Windows.Forms.Label();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.rateButtonBad = new System.Windows.Forms.RadioButton();
            this.groupBoxRate = new System.Windows.Forms.GroupBox();
            this.buttonVote = new System.Windows.Forms.Button();
            this.rateButtonExcellent = new System.Windows.Forms.RadioButton();
            this.rateButtonGood = new System.Windows.Forms.RadioButton();
            this.rateButtonLag = new System.Windows.Forms.RadioButton();
            this.serverLoopWorker = new System.ComponentModel.BackgroundWorker();
            this.serverrating = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.serverVoteWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.groupBoxRate.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server:";
            // 
            // currentserverip
            // 
            this.currentserverip.AutoSize = true;
            this.currentserverip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentserverip.Location = new System.Drawing.Point(59, 9);
            this.currentserverip.Name = "currentserverip";
            this.currentserverip.Size = new System.Drawing.Size(103, 13);
            this.currentserverip.TabIndex = 1;
            this.currentserverip.Text = "255.255.255.255";
            // 
            // rateButtonBad
            // 
            this.rateButtonBad.AutoSize = true;
            this.rateButtonBad.Location = new System.Drawing.Point(11, 13);
            this.rateButtonBad.Name = "rateButtonBad";
            this.rateButtonBad.Size = new System.Drawing.Size(43, 17);
            this.rateButtonBad.TabIndex = 2;
            this.rateButtonBad.TabStop = true;
            this.rateButtonBad.Text = "bad";
            this.rateButtonBad.UseVisualStyleBackColor = true;
            this.rateButtonBad.CheckedChanged += new System.EventHandler(this.rateButtonBad_CheckedChanged);
            // 
            // groupBoxRate
            // 
            this.groupBoxRate.Controls.Add(this.buttonVote);
            this.groupBoxRate.Controls.Add(this.rateButtonExcellent);
            this.groupBoxRate.Controls.Add(this.rateButtonGood);
            this.groupBoxRate.Controls.Add(this.rateButtonLag);
            this.groupBoxRate.Controls.Add(this.rateButtonBad);
            this.groupBoxRate.Location = new System.Drawing.Point(15, 25);
            this.groupBoxRate.Name = "groupBoxRate";
            this.groupBoxRate.Size = new System.Drawing.Size(310, 41);
            this.groupBoxRate.TabIndex = 3;
            this.groupBoxRate.TabStop = false;
            // 
            // buttonVote
            // 
            this.buttonVote.Location = new System.Drawing.Point(245, 10);
            this.buttonVote.Name = "buttonVote";
            this.buttonVote.Size = new System.Drawing.Size(54, 23);
            this.buttonVote.TabIndex = 6;
            this.buttonVote.Text = "Rate";
            this.buttonVote.UseVisualStyleBackColor = true;
            this.buttonVote.Click += new System.EventHandler(this.buttonVote_Click);
            // 
            // rateButtonExcellent
            // 
            this.rateButtonExcellent.AutoSize = true;
            this.rateButtonExcellent.Location = new System.Drawing.Point(172, 13);
            this.rateButtonExcellent.Name = "rateButtonExcellent";
            this.rateButtonExcellent.Size = new System.Drawing.Size(67, 17);
            this.rateButtonExcellent.TabIndex = 5;
            this.rateButtonExcellent.TabStop = true;
            this.rateButtonExcellent.Text = "excellent";
            this.rateButtonExcellent.UseVisualStyleBackColor = true;
            this.rateButtonExcellent.CheckedChanged += new System.EventHandler(this.rateButtonExcellent_CheckedChanged);
            // 
            // rateButtonGood
            // 
            this.rateButtonGood.AutoSize = true;
            this.rateButtonGood.Location = new System.Drawing.Point(117, 13);
            this.rateButtonGood.Name = "rateButtonGood";
            this.rateButtonGood.Size = new System.Drawing.Size(49, 17);
            this.rateButtonGood.TabIndex = 4;
            this.rateButtonGood.TabStop = true;
            this.rateButtonGood.Text = "good";
            this.rateButtonGood.UseVisualStyleBackColor = true;
            this.rateButtonGood.CheckedChanged += new System.EventHandler(this.rateButtonGood_CheckedChanged);
            // 
            // rateButtonLag
            // 
            this.rateButtonLag.AutoSize = true;
            this.rateButtonLag.Location = new System.Drawing.Point(61, 13);
            this.rateButtonLag.Name = "rateButtonLag";
            this.rateButtonLag.Size = new System.Drawing.Size(50, 17);
            this.rateButtonLag.TabIndex = 3;
            this.rateButtonLag.TabStop = true;
            this.rateButtonLag.Text = "laggy";
            this.rateButtonLag.UseVisualStyleBackColor = true;
            this.rateButtonLag.CheckedChanged += new System.EventHandler(this.rateButtonLag_CheckedChanged);
            // 
            // serverrating
            // 
            this.serverrating.AutoSize = true;
            this.serverrating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverrating.Location = new System.Drawing.Point(207, 9);
            this.serverrating.Name = "serverrating";
            this.serverrating.Size = new System.Drawing.Size(32, 13);
            this.serverrating.TabIndex = 4;
            this.serverrating.Text = "N.A.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Rate:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 72);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.serverrating);
            this.Controls.Add(this.groupBoxRate);
            this.Controls.Add(this.currentserverip);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "D4ServerTracker";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.groupBoxRate.ResumeLayout(false);
            this.groupBoxRate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label currentserverip;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.RadioButton rateButtonBad;
        private System.Windows.Forms.GroupBox groupBoxRate;
        private System.Windows.Forms.Button buttonVote;
        private System.Windows.Forms.RadioButton rateButtonExcellent;
        private System.Windows.Forms.RadioButton rateButtonGood;
        private System.Windows.Forms.RadioButton rateButtonLag;
        private System.ComponentModel.BackgroundWorker serverLoopWorker;
        private System.Windows.Forms.Label serverrating;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.BackgroundWorker serverVoteWorker;
    }
}

