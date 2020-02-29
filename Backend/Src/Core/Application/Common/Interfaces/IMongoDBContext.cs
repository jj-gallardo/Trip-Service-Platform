using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
using Trip.Domain.Entities;

namespace Api.Application.Common.Interfaces
{
    public interface IMongoDBContext
    {
        IMongoCollection<Card> Cards { get; }        
    }
}