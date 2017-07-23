using MongoDB.Bson;
using Common.Entities;

namespace MGSUBackend.Models.Mappers
{
    public static class UserMapper
    {
        public static UserModel UserToUserModel(User user)
        {
            if (user == null) return null;

            return new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role
            };
        }

        public static User UserModelToUser(UserModel userModel)
        {
            if (userModel == null) return null;

            return new User
            {
                Id = userModel.Id,
                FirstName = userModel.FirstName,
                MiddleName = userModel.MiddleName,
                LastName = userModel.LastName,
                Email = userModel.Email
            };
        }
    }
}