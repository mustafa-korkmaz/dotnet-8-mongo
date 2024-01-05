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
                map.MapProperty(x => x.Claims);
                map.MapMember(x => x.Username).SetIsRequired(true);
                map.MapMember(x => x.Email).SetIsRequired(true);
                map.MapMember(x => x.PasswordHash).SetIsRequired(true);
                map.MapMember(x => x.IsEmailConfirmed).SetIsRequired(true);
                map.SetIgnoreExtraElements(true);
            });
        }
    }
}
