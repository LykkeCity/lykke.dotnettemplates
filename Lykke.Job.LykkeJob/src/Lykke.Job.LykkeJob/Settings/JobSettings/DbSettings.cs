using Lykke.SettingsReader.Attributes;

namespace Lykke.Job.LykkeJob.Settings.JobSettings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }
    }
}
