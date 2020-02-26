using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
using Trip.Domain.Entities;

namespace Trip.Persistance
{
    public interface IMongoDBContext
    {
        IMongoCollection<Card> Cards { get; }        
    }
}