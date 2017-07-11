using System;
using System.ComponentModel.DataAnnotations;
using MGSUCore.Controllers.Extentions;
using BsonDateTime = MongoDB.Bson.BsonDateTime;

namespace MGSUCore.Models
{
    public class DonationModel
    {
        [Required]
		[ObjectId]
        public string UserId { get; set; }

		[Required]
		[ObjectId]		
		public string ProjectId { get; set; }

		[Required]
		public decimal Value { get; set; }

		public BsonDateTime Date { get; set; }

		public bool Recursive { get; set; }

		public bool Confirmed { get; set; }

		public string CreatingDate { get; set; }
    }
}
