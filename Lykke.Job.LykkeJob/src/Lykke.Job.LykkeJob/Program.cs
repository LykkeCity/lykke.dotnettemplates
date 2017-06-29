using Microsoft.Extensions.Configuration;

namespace Lykke.Job.LykkeJob
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddJsonFile($"appsettings.{AppHost.HostingEnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            var host = new AppHost(configuration);

            host.Run();
        }
    }
}