
using MongoDB.Driver;

namespace Infrastructure.Persistence.MongoDb
{
    public interface IMongoContext : IDisposable
    {
        IMongoCollection<TDocument> GetCollection<TDocument>();

        /// <summary>
        /// will be used if a transactional session is created
        /// </summary>
        /// <returns></returns>
        IClientSessionHandle? GetSession();

        /// <summary>
        /// will be used if a transactional session is required
        /// </summary>
        /// <returns></returns>
        Task<IClientSessionHandle> StartSessionAsync();
    }
}
