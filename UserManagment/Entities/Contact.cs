using System;
using Common;

namespace UserManagment.Entities
{
    public class Contact : PersistentEntity
    {
        public string Team { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Description { get; set; }
        public Uri Img { get; set; }
    }
}