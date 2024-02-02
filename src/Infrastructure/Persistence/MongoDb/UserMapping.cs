using Domain.Aggregates.User;
using MongoDB.Bson.Serialization;

namespace Infrastructure.Persistence.MongoDb
{
    public class UserMapping
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<User>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
            });
        }
    }
}
