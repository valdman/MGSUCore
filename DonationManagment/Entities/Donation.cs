using Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DonationManagment.Entities
{
    public class Donation : PersistentEntity
    {
        public ObjectId UserId { get; set; }
        
        public ObjectId ProjectId { get; set; }
        
        public decimal Value { get; set; }
        
        public BsonDateTime Date {get; set; }
        
        public bool Recursive { get; set; }
        
        public bool Confirmed { get; set; }
    }
}