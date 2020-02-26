using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Reflection;
using Trip.Persistance;
using Trip.Persistence.Configuration;

namespace Trip.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMongoDbPersistence(this IServiceCollection services,string connectionString, string dbName)
        {            
            services.AddSingleton<IMongoClient>(s => new MongoClient(connectionString));
            services.AddScoped<IMongoDBContext>(s => new MongoDBContext(s.GetRequiredService<IMongoClient>(), dbName));
            
            ConfigureCollectionBsonClassMappingInAssembly(typeof(MongoDBContext).Assembly);
            return services;
        }

        private static void ConfigureCollectionBsonClassMappingInAssembly(Assembly assembly)
        {
            ConventionRegistry.Register("CamelCase", new ConventionPack { new CamelCaseElementNameConvention() }, _ => true);

            var type = typeof(IMongoCollectionConfiguration);
            assembly.GetTypes()
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface).ToList()
                .ForEach(t =>
                {
                    ((IMongoCollectionConfiguration)Activator.CreateInstance(t)).Configure();
                });
        }
    }
}
