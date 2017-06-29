using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Lykke.JobTriggers.Triggers;
using Microsoft.AspNetCore.Hosting;

namespace Lykke.Job.LykkeJob
{
    class Program
    {
        static void Main(string[] args)
        {
            var webHostCancellationTokenSource = new CancellationTokenSource();
            IWebHost webHost = null;
            TriggerHost triggerHost = null;
            Task webHostTask = null;
            Task triggerHostTask = null;

            try
            {
                webHost = new WebHostBuilder()
                    .UseKestrel()
                    .UseUrls("http://*:5000")
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseStartup<Startup>()
                    .UseApplicationInsights()
                    .Build();

                triggerHost = new TriggerHost(webHost.Services);

                webHostTask = Task.Factory.StartNew(() => webHost.Run(webHostCancellationTokenSource.Token));
                triggerHostTask = triggerHost.Start();
                
                Task.WhenAny(webHostTask, triggerHostTask).Wait();
            }
            finally
            {
                Console.WriteLine("Terminating...");

                webHostCancellationTokenSource.Cancel();
                webHostTask?.Wait();
                triggerHost?.Cancel();
                triggerHostTask?.Wait();
            }
        }
    }
}