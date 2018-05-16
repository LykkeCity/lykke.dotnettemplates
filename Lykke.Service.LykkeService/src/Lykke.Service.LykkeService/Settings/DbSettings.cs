using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.LykkeService.Settings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }
    }
}
