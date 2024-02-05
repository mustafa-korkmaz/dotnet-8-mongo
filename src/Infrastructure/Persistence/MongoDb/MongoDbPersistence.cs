using Domain.Aggregates;
using Domain.Aggregates.Order;
using Domain.Aggregates.User;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Infrastructure.Persistence.MongoDb
{
    public static class MongoDbPersistence
    {
        public static async Task ConfigureAsync(IMongoContext mongoContext, ILogger logger)
        {
            logger.LogInformation("handling mongo collections mappings");

            BsonClassMap.RegisterClassMap<Document>(x =>
            {
                x.AutoMap();
                x.MapIdMember(document => document.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });

            UserMapping.Configure();
            ProductMapping.Configure();
            OrderMapping.Configure();

            // Set representation to Decimal128 for decimal types
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?),
                new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));

            logger.LogInformation("handling mongo collections indices");

            // Create userId index in orders
            var orders = mongoContext.GetCollection<Order>();
            var ordersIndexKeyDefinition = Builders<Order>.IndexKeys.Ascending(x => x.UserId);

            await orders.Indexes.CreateOneAsync(new CreateIndexModel<Order>(ordersIndexKeyDefinition));

            // Create unique email index in users
            var users = mongoContext.GetCollection<User>();
            var usersIndexKeysDefinition = Builders<User>.IndexKeys.Ascending(x => x.Email);

            await users.Indexes.CreateOneAsync(new CreateIndexModel<User>(usersIndexKeysDefinition,
                new CreateIndexOptions
                {
                    Unique = true
                }));
        }
    }
}