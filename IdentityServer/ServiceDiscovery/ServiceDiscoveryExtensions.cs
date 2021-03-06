﻿using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TripIdentityServer.Infrastructure.ServiceDiscovery
{
    public static class ServiceDiscoveryExtensions
    {
        public static void RegisterConsulServices(this IServiceCollection services, ServiceConfig serviceConfig)
        {
            if (serviceConfig == null)
            {
                throw new ArgumentNullException(nameof(serviceConfig));
            }

            services.AddSingleton<ServiceConfig>(serviceConfig);
            var consulClient = CreateConsulClient(serviceConfig);
            services.AddSingleton<IConsulClient, ConsulClient>(p => consulClient);
        }

        private static ConsulClient CreateConsulClient(ServiceConfig serviceConfig)
        {
            return new ConsulClient(config =>
            {
                config.Address = serviceConfig.ServiceDiscoveryAddress;
            });
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("AppExtensions");
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            var serviceConfig = app.ApplicationServices.GetRequiredService<ServiceConfig>();

            var name = Dns.GetHostName(); // get container id
            var serviceIp = Dns.GetHostEntry(name).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork).ToString();

            var registration = new AgentServiceRegistration
            {
                ID = $"{serviceConfig.ServiceName}-{Guid.NewGuid().ToString()}",
                Name = serviceConfig.ServiceName,
                Address = serviceIp,
                Port = serviceConfig.ServiceAddress.Port,
                Tags = new[] { "trip", "identity-server" },
                Checks = new AgentServiceCheck[] {
                    new AgentCheckRegistration()
                        {
                            HTTP = $"{serviceConfig.ServiceAddress.Scheme}://{serviceIp}:{serviceConfig.ServiceAddress.Port}/health",
                            Notes = "Checks /health status",
                            Timeout = TimeSpan.FromSeconds(3) ,
                            Interval = TimeSpan.FromSeconds(10)
                        }
                }
            };

            logger.LogInformation("Registering with Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Unregistering from Consul");
                consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            });

            return app;
        }
    }
}