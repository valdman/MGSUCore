using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace MGSUCore.Models
{
    public class DonationModel
    {
        [Required]
        public string UserId { get; set; }

		[Required]
		public string ProjectId { get; set; }

		[Required]
		public decimal Value { get; set; }

		public BsonDateTime Date { get; set; }

		public bool Recursive { get; set; }

		public bool Confirmed { get; set; }

		public string CreatingTime { get; set; }
    }
}
