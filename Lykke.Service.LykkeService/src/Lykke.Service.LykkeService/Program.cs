using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Lykke.Service.LykkeService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"LykkeService version {Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationVersion}");
//#$if DEBUG
            Console.WriteLine("Is DEBUG");
//#$else
            //$#$//Console.WriteLine("Is RELEASE");
//#$endif           
            Console.WriteLine($"ENV_INFO: {Environment.GetEnvironmentVariable("ENV_INFO")}");

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://*:5000")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();

            Console.WriteLine("Terminated");
        }
    }
}
