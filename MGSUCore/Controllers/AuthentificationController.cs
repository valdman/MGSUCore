using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
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
			if (_accountManager.Authenticate(credentials))
			{
				var identity = new ClaimsIdentity("password");
				identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
				HttpContext.Authentication.SignInAsync("ApiAuth", new ClaimsPrincipal(identity)).Wait();
			}
			else
			{
				return Unauthorized();
			}
			return Ok(_accountManager.Authenticate(credentials));

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