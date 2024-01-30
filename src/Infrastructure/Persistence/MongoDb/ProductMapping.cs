using Domain.Aggregates.Product;
using MongoDB.Bson.Serialization;

namespace Infrastructure.Persistence.MongoDb
{
    public class ProductMapping
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Product>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
            });
        }
    }
}
