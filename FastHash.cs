using DeviceId;
using System.Text;

namespace D4ServerRate
{
    class FastHash
    {
        public static string CalculateUUID()
        {
            string uuid = new DeviceIdBuilder()
                .AddMachineName()
                .AddMacAddress()
                .AddOsVersion()
                .OnWindows(windows => windows
                    .AddProcessorId()
                    .AddMotherboardSerialNumber()
                    .AddSystemDriveSerialNumber())
                .AddUserName()
                .ToString();

            byte[] inputBytes = Encoding.UTF8.GetBytes(uuid);
            uint hash = 2166136261;

            for (int i = 0; i < inputBytes.Length; i++)
            {
                hash = (hash * 16777619) ^ inputBytes[i];
            }

            return hash.ToString("X8");
        }
    }
}
