using Common;
using FileManagment.Entities;
using MongoDB.Bson;

namespace PostManagment.Entities
{
    public class Post : PersistentEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public Image Img { get; set; }
        public string Category { get; set; }
        public BsonDateTime Date { get; set; }
    }
}