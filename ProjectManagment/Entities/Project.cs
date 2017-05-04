using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectManagment.Entities
{
    public class Project
    {
        public string Name { get; private set; }
        public string Direction { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        
        public BsonDateTime CreatingDate { get; set; }
        
        public decimal Need { get; set; }
        
        public decimal Givein { get; set; }
        
        public bool Public { get; set; }

    }
}