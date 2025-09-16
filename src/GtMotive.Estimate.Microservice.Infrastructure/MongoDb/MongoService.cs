using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb
{
    public class MongoService
    {
        private static readonly object LockObject = new object();
        private static bool _isRegistered = false;

        public MongoService(IOptions<MongoDbSettings> options)
        {
            MongoClient = new MongoClient(options.Value.ConnectionString);

            // Register BSON serializers for Value Objects
            RegisterBsonClasses();
        }

        public MongoClient MongoClient { get; }

        /// <summary>
        /// Registers BSON serializers for Domain Value Objects.
        /// This prevents MongoDB serialization errors for custom value types.
        /// </summary>
        private static void RegisterBsonClasses()
        {
            // Ensure registration happens only once to avoid conflicts
            lock (LockObject)
            {
                if (_isRegistered)
                    return;

                // Register serializers for Guid-based Value Objects
                BsonClassMap.RegisterClassMap<VehicleId>(cm =>
                {
                    cm.AutoMap();
                    cm.MapMember(x => x.Value)
                        .SetSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
                });

                BsonClassMap.RegisterClassMap<RentalId>(cm =>
                {
                    cm.AutoMap();
                    cm.MapField("_value")
                        .SetSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
                });

                BsonClassMap.RegisterClassMap<CustomerId>(cm =>
                {
                    cm.AutoMap();
                    cm.MapField("_value")
                        .SetSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
                });

                // Register serializers for String-based Value Objects
                BsonClassMap.RegisterClassMap<CustomerName>(cm =>
                {
                    cm.AutoMap();
                    cm.MapField("_value")
                        .SetSerializer(new StringSerializer());
                });

                BsonClassMap.RegisterClassMap<CustomerEmail>(cm =>
                {
                    cm.AutoMap();
                    cm.MapField("_value")
                        .SetSerializer(new StringSerializer());
                });

                _isRegistered = true;
            }
        }
    }
}
