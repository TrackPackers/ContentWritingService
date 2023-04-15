using ContentWriterService.Models;
using MongoDB.Driver;

namespace ContentWriterService.Context.Interfaces
{
    public interface IDbContentContext
    {
        public IMongoCollection<Content> Contents { get; }
    }
}
