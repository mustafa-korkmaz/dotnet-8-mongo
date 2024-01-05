using Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Persistence.MongoDb
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase _database;
        private IClientSessionHandle? _session;

        private readonly IMongoClient _mongoClient;

        public MongoContext(IMongoClient mongoClient, IOptions<MongoDbConfig> dbConfigOptions)
        {
            var dbConfig = dbConfigOptions.Value;
            _mongoClient = mongoClient;
            _database = _mongoClient.GetDatabase(dbConfig.DatabaseName);
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>()
        {
            return _database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private string GetCollectionName(Type documentType)
        {
            var shortType = documentType.Name;

            char[] array = shortType.ToCharArray();

            array[0] = char.ToLower(array[0]);

            var singularCollectionName = new string(array);

            var pluralCollectionName = array[array.Length - 1] == 's' ? singularCollectionName + "es" : singularCollectionName + "s";

            return pluralCollectionName;
        }

        public async Task<IClientSessionHandle> StartSessionAsync()
        {
            _session = await _mongoClient.StartSessionAsync();

            return _session;
        }

        public IClientSessionHandle? GetSession()
        {
            return _session;
        }

        public void Dispose()
        {
            _session?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
