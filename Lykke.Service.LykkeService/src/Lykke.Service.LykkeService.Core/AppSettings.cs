namespace Lykke.Service.LykkeService.Core
{
    public class AppSettings
    {
        public LykkeServiceSettings LykkeServiceService { get; set; }
    }

    public class LykkeServiceSettings
    {
        public DbSettings Db { get; set; }
    }

    public class DbSettings
    {
        public string LogsConnString { get; set; }
    }
}
