using System;
using Autofac;
using Common.Log;

namespace Lykke.Service.LykkeService.Client
{
    public static class AutofacExtension
    {
        public static void RegisterLykkeServiceClient(this ContainerBuilder builder, string serviceUrl, ILog log)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (log == null) throw new ArgumentNullException(nameof(log));
            if (string.IsNullOrWhiteSpace(serviceUrl))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceUrl));

            builder.RegisterType<LykkeServiceClient>()
                .WithParameter("serviceUrl", serviceUrl)
                .As<ILykkeServiceClient>()
                .SingleInstance();
        }

        public static void RegisterLykkeServiceClient(this ContainerBuilder builder, LykkeServiceServiceClientSettings settings, ILog log)
        {
            builder.RegisterLykkeServiceClient(settings?.ServiceUrl, log);
        }
    }
}
