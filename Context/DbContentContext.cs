using ContentWriterService.Context.Interfaces;
using ContentWriterService.Models;
using ContentWriterService.Services;
using MongoDB.Driver;

namespace ContentWriterService.Context
{
    public class DbContentContext : MongoClient, IDbContentContext
    {
        private readonly IMongoDatabase _database;
        public IMongoCollection<Content> Contents { get; }

        public DbContentContext(string connectionString, string databaseName) : base(connectionString)
        {
            _database = GetDatabase(databaseName);
            Contents = _database.GetCollection<Content>("contents");
        }

    }
}
