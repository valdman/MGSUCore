using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common;
using MGSUBackend.Models;
using MGSUBackend.Models.Mappers;
using MongoDB.Bson;
using UserManagment.Application;
using UserManagment.Entities;

namespace MGSUBackend.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        // GET: User
        public IEnumerable<UserModel> Get()
        {
            return _userManager.GetUserByPredicate()
                .Select(UserMapper.UserToUserModel);
        }

        // GET: User/5
        public IHttpActionResult Get(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _userManager.GetUserById(new ObjectId(id));
            if (user == null)
                return NotFound();

            return Ok(UserMapper.UserToUserModel(user));
        }

        // POST: User
        public IHttpActionResult Post([FromBody] UserRegistrationModel userModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!Password.IsStringCorrectPassword(userModel.Password))
                return BadRequest("Password is not satisfy security requirements");

            var userToCreate = new User
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                MiddleName = userModel.MiddleName,
                CreationTime = new BsonDateTime(DateTime.UtcNow),
                Email = userModel.Email,
                IsConfirmed = false,
                Phone = userModel.Phone,
                Password = new Password(userModel.Password),
                Role = UserRole.User
            };

            var id = _userManager.CreateUser(userToCreate);

            return Ok(id);
        }

        // PUT: User/5
        public IHttpActionResult Put(string id, [FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_userManager.GetUserById(new ObjectId(id)) == null)
                return NotFound();

            var userToUpdateNew = UserMapper.UserModelToUser(userModel);
            _userManager.UpdateUser(userToUpdateNew);

            return Ok();
        }

        // DELETE: User/5
        public IHttpActionResult Delete(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_userManager.GetUserById(new ObjectId(id)) == null)
                return NotFound();

            _userManager.DeleteUser(new ObjectId(id));

            return Ok();
        }
    }
}