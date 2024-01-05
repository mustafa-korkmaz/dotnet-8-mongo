using Domain.Aggregates;
using Domain.Aggregates.User;
using Infrastructure.Persistence.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IMongoContext context) : base(context)
        {

        }

        public async Task<ListDocumentResponse<User>> SearchAsync(int offset, int limit, string? searchText)
        {
            var response = new ListDocumentResponse<User>();

            FilterDefinition<User> filter;

            if (searchText != null)
            {
                filter = Builders<User>.Filter
                    .Where(doc => doc.Email.Contains(searchText) ||
                                  doc.NameSurname!.Contains(searchText));
            }
            else
            {
                filter = new BsonDocument();
            }

            var docs = Collection.Find(filter);

            response.RecordsTotal = await docs.CountDocumentsAsync();

            if (response.RecordsTotal > 0)
            {
                response.Items = await docs
                     .SortByDescending(p => p.Id)
                     .Skip(offset)
                     .Limit(limit)
                     .ToListAsync();
            }

            return response;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email);

            return await Collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Username, username);

            return await Collection.Find(filter).SingleOrDefaultAsync();
        }
    }
}