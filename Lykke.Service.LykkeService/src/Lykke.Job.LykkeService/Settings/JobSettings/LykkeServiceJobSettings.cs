namespace Lykke.Job.LykkeService.Settings.JobSettings
{
    public class LykkeServiceJobSettings
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
