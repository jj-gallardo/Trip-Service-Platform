using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trip.Domain.Entities;
using Trip.Persistence.Configuration;

namespace Trip.Persistance 
{ 
    public class MongoDBContext : IMongoDBContext
    {
        //public static string ConnectionString { get; set; } = "mongodb://trip-database:27017";
        public static string ConnectionString { get; set; } = "mongodb://localhost:27017";
        public static string DatabaseName { get; set; } = "trip";
        public static bool IsSSL { get; set; }

        private IMongoDatabase _database { get; }

        public MongoDBContext(IMongoClient mongoClient, string dbName)
        {
            try
            {                
                _database = mongoClient.GetDatabase(dbName);                
            }
            catch (Exception ex)
            {
                throw new Exception("Can not access to db server.", ex);
            }
        }

       public IMongoCollection<Card> Cards
       {
           get
           {
               return _database.GetCollection<Card>("trip-cards");
           }
       }
    }
}
