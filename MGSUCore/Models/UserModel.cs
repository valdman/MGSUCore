using System;
using System.ComponentModel.DataAnnotations;
using Common.Entities;
using MGSUCore.Controllers.Extentions;

namespace MGSUBackend.Models
{
    public class UserModel
    {
        [ObjectId]
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public UserRole Role { get; set; }

        public DateTime CreatingDate { get; set; }
    }
}