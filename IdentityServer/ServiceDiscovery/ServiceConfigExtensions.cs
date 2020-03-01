using System;
using Microsoft.Extensions.Configuration;

namespace TripIdentityServer.Infrastructure.ServiceDiscovery
{
    public static class ServiceConfigExtensions
    {
        public static ServiceConfig GetServiceConfig(this IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var serviceConfig = new ServiceConfig
            {
                ServiceDiscoveryAddress = new Uri(configuration["ServiceConfig:serviceDiscoveryAddress"]),
                ServiceAddress = new Uri(configuration["ServiceConfig:serviceAddress"]),
                ServiceName = configuration["ServiceConfig:serviceName"],
                ServiceId = configuration["ServiceConfig:serviceId"]
            };

            return serviceConfig;
        }
    }
}