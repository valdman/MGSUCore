﻿using System.ComponentModel.DataAnnotations;

namespace MGSUBackend.Models
{
    public class UserAuthModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}