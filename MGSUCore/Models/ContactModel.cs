using System;
using MGSUBackend.Models;

namespace MGSUCore.Models
{
    public class ContactModel
    {
        public string Id {get; set;}
        public string Team { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Description { get; set; }
        public ImageModel Img { get; set; }

        public DateTime CreatingDate {get; set;}
    }
}