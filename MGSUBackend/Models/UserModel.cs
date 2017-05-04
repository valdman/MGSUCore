using System.ComponentModel.DataAnnotations;

namespace MGSUBackend.Models
{
    public class UserModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}