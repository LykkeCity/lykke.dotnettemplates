namespace Lykke.Job.LykkeJob.Core.Settings.JobSettings
{
    public class DbSettings
    {
        public string LogsConnString { get; set; }
#if rabbitpub
        public string SnapshotsConnectionString { get; set; }
#endif
    }
}