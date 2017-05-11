using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Common;
using MGSUBackend.Authentification;
using MGSUBackend.Models;
using MGSUBackend.Models.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using UserManagment.Application;
using UserManagment.Entities;

namespace MGSUCore.Controllers
{
    [Route("[controller]")]
    public class AuthentificationController : Controller
    {
        private readonly ISessionManager _sessionManager;
        private readonly IUserManager _userManager;

        public AuthentificationController(ISessionManager sessionManager, IUserManager userManager)
        {
            _sessionManager = sessionManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] Credentials credentials)
        {
            var userWhoIntented = _userManager.GetUserByPredicate(user => user.Email == credentials.Email)
                .SingleOrDefault(); //todo: to special domain method
            if (userWhoIntented == null)
                return NotFound();

            if (!userWhoIntented.Password.Equals(new Password(credentials.Password)))
                return Unauthorized();

            var currentSession = _sessionManager.GetSessionForUser(userWhoIntented.Id);

            Response.Cookies.Append(Session.CookieName, currentSession.Sid.ToString());
            return Ok();
        }

        [HttpPost]
        [Route("logout")]
        [Authorization(UserRole.User)]
        public IActionResult Logout()
        {
            if (!Request.Cookies.TryGetValue(Session.CookieName, out string sessionId) || sessionId == string.Empty)
            {
                return Unauthorized();
            }

            _sessionManager.EndSessionbyId(Guid.Parse(sessionId));
            return Ok();
        }

        [HttpGet]
        [Route("current")]
        [Authorization(UserRole.User)]
        public IActionResult Current()
        {
            var currentUserId = User.Identity.GetId();

            var currentUser = _userManager.GetUserById(currentUserId);

            if (currentUser == null)
            {
                return Unauthorized();
            }

            return Ok(UserMapper.UserToUserModel(currentUser));
        }
    }
}