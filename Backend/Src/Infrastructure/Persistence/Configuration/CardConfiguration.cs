using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;
using Trip.Domain.Entities;

namespace Trip.Persistence.Configuration
{
    public class CardConfiguration : IMongoCollectionConfiguration
    {
        public void Configure()
        {
            BsonClassMap.RegisterClassMap<Card>(cm =>
            {
                cm.MapIdProperty(c => c.Id).SetElementName("_id").SetIdGenerator(new StringObjectIdGenerator());
                cm.MapMember(c => c.Title).SetElementName("title");
                cm.MapMember(c => c.Description).SetElementName("description");                
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}
