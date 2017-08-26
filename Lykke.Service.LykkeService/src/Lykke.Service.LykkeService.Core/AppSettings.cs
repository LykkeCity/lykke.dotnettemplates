namespace Lykke.Service.LykkeService.Core
{
    public class AppSettings
    {
        public LykkeServiceSettings LykkeServiceService { get; set; }
        public SlackNotificationsSettings SlackNotifications { get; set; }
    }

    public class LykkeServiceSettings
    {
        public DbSettings Db { get; set; }
    }

    public class DbSettings
    {
        public string LogsConnString { get; set; }
    }

    public class SlackNotificationsSettings
    {
        public AzureQueueSettings AzureQueue { get; set; }
    }

    public class AzureQueueSettings
    {
        public string ConnectionString { get; set; }

        public string QueueName { get; set; }
    }
}
