using Api.Application.Common.Interfaces;
using Application;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Trip.Infrastructure;
using Trip.Infrastructure.ServiceDiscovery;
using Trip.Persistence;
using Trip.WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().AddAuthorization();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration.GetSection("ApiAuthorization")["AuthorizationServerAddress"];
                    options.ApiName = Configuration.GetSection("ApiAuthorization")["ApiName"];
                    options.RequireHttpsMetadata = false;
                });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllers();
            services.AddTransient<ICurrentUserService, CurrentUserService>();                        
            services.AddInfrastructure(Configuration);
            services.AddMongoDbPersistence(Configuration.GetConnectionString("MongoDb"), Configuration["dbName"]);
            services.AddApplication();
            services.AddHealthChecks();                    
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Trip API", Version = "v1" });
            });

        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Trip API v1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
                                    
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
            
            if(bool.Parse(Configuration["EnableServiceDiscovery"]))
                app.UseConsul();
        }
    }
}
