using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.LykkeService.Settings.ServiceSettings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }
    }
}
