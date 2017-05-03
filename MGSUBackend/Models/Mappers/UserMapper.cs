using System.Net.Mail;
using UserManagment.Entities;

namespace MGSUBackend.Models.Mappers
{
    public static class UserMapper
    {
        public static UserModel UserToUserModel(User user)
        {
            return new UserModel
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email.Address
            };
        }

        public static User UserModelToUser(UserModel userModel)
        {
            return new User
            {
                FirstName = userModel.FirstName,
                MiddleName = userModel.MiddleName,
                LastName = userModel.LastName,
                Email = new MailAddress(userModel.Email)
            };
        }
    }
}