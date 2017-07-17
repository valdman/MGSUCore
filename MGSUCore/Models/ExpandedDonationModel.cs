using System.ComponentModel.DataAnnotations;
using MGSUBackend.Models;
using MGSUCore.Controllers.Extentions;
using MongoDB.Bson;

namespace MGSUCore.Models
{
    public class ExpandedDonationModel
    {
		[Required]
		[ObjectId]
		public string Id { get; set; }

        [Required]
        public UserModel User { get; set; }

		[Required]	
		public ProjectModel Project { get; set; }

		[Required]
		public decimal Value { get; set; }

		public string Date { get; set; }

		public bool Recursive { get; set; }

		public bool Confirmed { get; set; }

		public string CreatingDate { get; set; }
    }
}