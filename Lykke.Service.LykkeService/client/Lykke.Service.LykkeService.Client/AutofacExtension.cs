using System;
using Autofac;
using JetBrains.Annotations;
using Lykke.HttpClientGenerator;

namespace Lykke.Service.LykkeService.Client
{
    [PublicAPI]
    public static class AutofacExtension
    {
        /// <summary>
        /// Registers <see cref="ILykkeServiceClient"/> in Autofac container using <see cref="LykkeServiceServiceClientSettings"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="settings"></param>
        /// <param name="builderConfigure"></param>
        public static void RegisterLykkeServiceClient(
            this ContainerBuilder builder,
            LykkeServiceServiceClientSettings settings,
            Func<HttpClientGeneratorBuilder, HttpClientGeneratorBuilder> builderConfigure)
        {
            builder.RegisterClient<ILykkeServiceClient>(settings?.ServiceUrl, builderConfigure);
        }
    }
}
