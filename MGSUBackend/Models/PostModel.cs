using System.ComponentModel.DataAnnotations;

namespace MGSUBackend.Models
{
    public class PostModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        public ImageModel Img { get; set; }
        public string Category { get; set; }

        [DataType(DataType.DateTime)]
        public string Date { get; set; }

        [DataType(DataType.DateTime)]
        public string CreatingTime { get; set; }
    }
}