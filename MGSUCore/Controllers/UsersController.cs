﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using MGSUBackend.Authentification;
using MGSUBackend.Models;
using MGSUBackend.Models.Mappers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using UserManagment.Application;
using UserManagment.Entities;

namespace MGSUCore.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        // GET: Users
        public IEnumerable<UserModel> Get()
        {
            return _userManager.GetUserByPredicate()
                .Select(UserMapper.UserToUserModel);
        }

        // GET: Users/5
        public IActionResult Get(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _userManager.GetUserById(new ObjectId(id));
            if (user == null)
                return NotFound();

            return Ok(UserMapper.UserToUserModel(user));
        }

        // POST: Users
        public IActionResult Post([FromBody] UserRegistrationModel userModel)
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

        // PUT: Users/5
        [Authorization(UserRole.User)]
        public IActionResult Put(string id, [FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.Identity.GetId();

            if (userId.ToString() != id)
                return Unauthorized();
            
            if (_userManager.GetUserById(new ObjectId(id)) == null)
                return NotFound();

            userModel.Id = userId.ToString();

            var userToUpdateNew = UserMapper.UserModelToUser(userModel);
            _userManager.UpdateUser(userToUpdateNew);

            return Ok();
        }

        // DELETE: Users/5
        [Authorization(UserRole.User)]
        public IActionResult Delete(string id)
        {
            if (User.Identity.GetId().ToString() != id)
                return Unauthorized();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_userManager.GetUserById(new ObjectId(id)) == null)
                return NotFound();

            _userManager.DeleteUser(new ObjectId(id));

            return Ok();
        }
    }
}