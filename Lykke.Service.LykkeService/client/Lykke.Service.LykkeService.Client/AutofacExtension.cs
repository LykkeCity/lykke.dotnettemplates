using System;
using Autofac;
using Autofac.Core;
using JetBrains.Annotations;
using Lykke.Common.Log;

namespace Lykke.Service.LykkeService.Client
{
    [PublicAPI]
    public static class AutofacExtension
    {
        public static void RegisterLykkeServiceClient(this ContainerBuilder builder, string serviceUrl)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (string.IsNullOrWhiteSpace(serviceUrl))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceUrl));
            }

            builder.RegisterType<LykkeServiceClient>()
                .WithParameter("serviceUrl", serviceUrl)
                .As<ILykkeServiceClient>()
                .SingleInstance();
        }

        public static void RegisterLykkeServiceClient(this ContainerBuilder builder, LykkeServiceServiceClientSettings settings)
        {
            builder.RegisterLykkeServiceClient(settings?.ServiceUrl);
        }
    }
}
