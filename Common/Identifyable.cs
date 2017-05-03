using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Common
{
    public abstract class Identifyable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; private set; }

    }
}