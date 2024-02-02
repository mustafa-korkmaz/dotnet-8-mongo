using Domain.Aggregates;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Persistence.MongoDb
{
    public static class MongoDbPersistence
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Document>(x =>
            {
                x.AutoMap();
                x.MapIdMember(document => document.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });

            ProductMapping.Configure();
            OrderMapping.Configure();
            
            // Set representation to Decimal128 for decimal types
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));

            // Ignore null values
            ConventionRegistry.Register("Ignore",
                new ConventionPack
                {
                    new IgnoreIfNullConvention(true)
                },
                t => true);

        }
    }
}