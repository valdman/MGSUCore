using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Common;
using MGSUBackend.Authentification;
using MGSUBackend.Models;
using MGSUBackend.Models.Mappers;
using UserManagment.Application;
using UserManagment.Entities;

namespace MGSUBackend.Controllers
{
    public class AuthentificationController : ApiController
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
        public HttpResponseMessage Login([FromBody] Credentials credentials)
        {
            var userWhoIntented = _userManager.GetUserByPredicate(user => user.Email == credentials.Email)
                .SingleOrDefault(); //todo: to special domain method
            if (userWhoIntented == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            if (!userWhoIntented.Password.Equals(new Password(credentials.Password)))
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var currentSession = _sessionManager.GetSessionForUser(userWhoIntented.Id);

            var cookie = new CookieHeaderValue(Session.CookieName, currentSession.Sid.ToString())
            {
                Expires = currentSession.ExpireTime,
                Domain = Request.RequestUri.Host,
                Path = "/"
            };

            var response = Request.CreateResponse(HttpStatusCode.OK, currentSession.UserId.ToString());
            response.Headers.AddCookies(new[] {cookie});
            return response;
        }

        [HttpPost]
        [Route("logout")]
        [Authorization(UserRole.User)]
        public HttpResponseMessage Logout()
        {
            var sessionId = Request.Headers.GetCookies(Session.CookieName)
                .FirstOrDefault()
                ?.Cookies.FirstOrDefault()
                ?.Value;

            if (sessionId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            _sessionManager.EndSessionbyId(Guid.Parse(sessionId));
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("current")]
        [Authorization(UserRole.User)]
        public HttpResponseMessage Current()
        {
            var currentUserId = User.Identity.GetId();

            var currentUser = _userManager.GetUserById(currentUserId);

            return currentUser == null
                ? Request.CreateResponse(HttpStatusCode.Unauthorized)
                : Request.CreateResponse(HttpStatusCode.OK, UserMapper.UserToUserModel(currentUser));
        }
    }
}