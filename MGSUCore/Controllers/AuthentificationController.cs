using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using Common;
using MGSUBackend.Authentification;
using MGSUBackend.Models;
using MGSUBackend.Models.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using UserManagment.Application;
using UserManagment.Entities;

namespace MGSUCore.Controllers
{
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
            var intentedUser = _userManager.GetUserByPredicate(user => user.Email == credentials.Email).Single();
            if(intentedUser == null)
            {
                return NotFound();
            }

            if(!intentedUser.Password.Equals(new Password(credentials.Password)))
            {
                return Unauthorized();
            }
            
            var myclaims = new List<Claim>(new Claim[] 
            { 
                new Claim(ClaimTypes.NameIdentifier, intentedUser.Id.ToString()),
                new Claim(ClaimTypes.Role, intentedUser.Role.ToString())
            });


            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(myclaims, "WebCookieAuthMiddleware"));

            HttpContext.Authentication.SignInAsync("WebCookieAuthMiddleware", claimsPrincipal).Wait();

            return Ok();
        }

        [HttpPost]
        [Route("logout")]
        [Authorize("User")]
        public IActionResult Logout()
        {
            HttpContext.Authentication.SignOutAsync("WebCookieAuthMiddleware");
            return Ok();
        }

        [HttpGet]
        [Route("current")]
        [Authorize("User")]
        public IActionResult Current()
        {
            var currentUserId = User.GetId();

            var currentUser = _userManager.GetUserById(currentUserId);

            if (currentUser == null)
            {
                return Unauthorized();
            }

            return Ok(UserMapper.UserToUserModel(currentUser));
        }
    }
}