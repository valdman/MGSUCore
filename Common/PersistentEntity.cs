using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Common
{
    public abstract class PersistentEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public BsonDateTime CreatingTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}