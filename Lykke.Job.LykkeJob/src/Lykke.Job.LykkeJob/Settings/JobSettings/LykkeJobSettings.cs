namespace Lykke.Job.LykkeJob.Settings.JobSettings
{
    public class LykkeJobSettings
    {
        public DbSettings Db { get; set; }
#if azurequeuesub
        public AzureQueueSettings AzureQueue { get; set; }
#endif
#if (rabbitsub || rabbitpub)
        public RabbitMqSettings Rabbit { get; set; }
#endif
    }
}
