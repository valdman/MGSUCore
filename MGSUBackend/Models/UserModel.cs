using System.ComponentModel.DataAnnotations;

namespace MGSUBackend.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}