using System.IO;
using Lykke.Service.IpGeoLocation.Web;
using Microsoft.AspNetCore.Hosting;

namespace Lykke.Service.LykkeService.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://*:5000")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
