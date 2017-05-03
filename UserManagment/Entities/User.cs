using System.Net.Mail;
using Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserManagment.Entities
{
    public class User : Identifyable
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public MailAddress Email { get; set; }
    }
}