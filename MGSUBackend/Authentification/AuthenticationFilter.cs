using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using MongoDB.Bson;
using UserManagment.Application;
using UserManagment.Entities;

namespace MGSUBackend.Authentification
{
    public class AuthenticationFilter : IAuthenticationFilter
    {
        private readonly ISessionManager _sessionManager;
        private readonly IUserManager _userManager;

        public AuthenticationFilter(ISessionManager sessionManager, IUserManager userManager)
        {
            _sessionManager = sessionManager;
            _userManager = userManager;
        }

        public bool AllowMultiple => true;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var tokenString = context?.Request?.Headers?.GetCookies(Session.CookieName)
                ?.FirstOrDefault()
                ?.Cookies?.FirstOrDefault()
                ?.Value;

            if (string.IsNullOrEmpty(tokenString))
            {
                Debug.WriteLine("Empty token");
                SetupUnauthenticated();
                return;
            }

            var sessionInfo = _sessionManager.GetSessionBySid(Guid.Parse(tokenString));

            if (sessionInfo == null)
            {
                Debug.WriteLine("Null sesh info");
                context.ErrorResult = new UnauthorizedResult(
                    new AuthenticationHeaderValue[] { },
                    context.Request);
                await context.ErrorResult.ExecuteAsync(cancellationToken);
                SetupUnauthenticated();
                return;
            }

            var currentUser = _userManager.GetUserById(sessionInfo.UserId);

            var identity = new Identitiy(sessionInfo.UserId, true);
            var principal = new Principal(currentUser.Role, identity);

            Thread.CurrentPrincipal = principal;
            Debug.WriteLine("Principal given: ");
            Debug.WriteLine(principal.ToJson());
            context.Principal = principal;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void SetupUnauthenticated()
        {
            Thread.CurrentPrincipal = Principal.EmptyPrincipal;
        }
    }
}