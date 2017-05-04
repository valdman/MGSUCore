using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DataAccess.Application;
using Journalist;
using MongoDB.Bson;
using UserManagment.Application;
using UserManagment.Entities;

namespace UserManagment
{
    public class UserManager : IUserManager
    {
        private readonly IRepository<User> _userRepository;

        public UserManager(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserById(ObjectId id)
        {
            Require.NotNull(id, nameof(id));

            return _userRepository.GetById(id);
        }

        public IEnumerable<User> GetUserByPredicate(Expression<Func<User, bool>> predicate = null)
        {
            return _userRepository.GetByPredicate(predicate);
        }

        public ObjectId CreateUser(User userToCreate)
        {
            Require.NotNull(userToCreate, nameof(userToCreate));

            return _userRepository.Create(userToCreate);
        }

        public void UpdateUser(User userToUpdate)
        {
            Require.NotNull(userToUpdate, nameof(userToUpdate));

            _userRepository.Update(userToUpdate);
        }

        public void DeleteUser(ObjectId userId)
        {
            Require.NotNull(userId, nameof(userId));

            _userRepository.Delete(userId);
        }
    }
}