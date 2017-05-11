using System;
using DataAccess.Application;
using Journalist;
using MongoDB.Bson;
using UserManagment.Application;
using UserManagment.Entities;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

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

            if (_userRepository.GetByPredicate(user => user.Email == userToCreate.Email).Any())
            {
                throw new PolicyException("User with this email already registered");
            }

            return _userRepository.Create(userToCreate);
        }

        public void UpdateUser(User userToUpdate)
        {
            Require.NotNull(userToUpdate, nameof(userToUpdate));

            if (_userRepository.GetByPredicate(user => user.Email == userToUpdate.Email && !user.Id.Equals(userToUpdate.Id)).Any())
            {
                throw new PolicyException("User with this email already exists");
            }

            _userRepository.Update(userToUpdate);
        }

        public void DeleteUser(ObjectId userId)
        {
            Require.NotNull(userId, nameof(userId));

            _userRepository.Delete(userId);
        }
    }
}