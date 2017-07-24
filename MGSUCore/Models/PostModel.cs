using System;
using System.ComponentModel.DataAnnotations;
using MGSUCore.Controllers.Extentions;
using MGSUCore.Models.Convertors;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace MGSUBackend.Models
{
    public class PostModel
    {
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        public ImageModel Img { get; set; }

        [Required]
        public string Category { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset Date { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatingDate { get; set; }
    }
}