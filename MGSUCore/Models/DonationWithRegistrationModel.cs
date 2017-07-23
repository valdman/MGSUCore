using System;
using System.ComponentModel.DataAnnotations;
using MGSUCore.Controllers.Extentions;
using MongoDB.Bson;

namespace MGSUCore.Models
{
    public class DonationWithRegistrationModel
    {
        [Required]
        [ObjectId]
        public string ProjectId { get;  set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        
        [Required]        
        public string LastName { get; set; }

        [Required]        
        [EmailAddress]
        public string Email { get; set; }

        [Required]  
        public decimal Value { get; set; }

        [Required]        
        public bool Recursive { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool Confirmed { get; set; }
    }
}