using Lykke.SettingsReader.Attributes;

namespace Lykke.Job.LykkeService.Settings.JobSettings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }
    }
}
