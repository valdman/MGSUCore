using System;
using System.ComponentModel.DataAnnotations;
using MGSUCore.Controllers.Extentions;

namespace MGSUBackend.Models
{
    public class PostModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        public ImageModel Img { get; set; }

        [Required]
        public string Category { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatingDate { get; set; }
    }
}