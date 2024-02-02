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
                map.MapMember(user => user.NameSurname).SetIgnoreIfNull(true);
                map.MapMember(user => user.Claims).SetIgnoreIfNull(true);
                map.SetIgnoreExtraElements(true);
            });
        }
    }
}
