using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using UserManagment.Entities;

namespace MGSUBackend.Authentification
{
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        private readonly UserRole _accountRole;

        public AuthorizationAttribute(UserRole accountRole)
        {
            _accountRole = accountRole;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return Thread.CurrentPrincipal.IsInRole(_accountRole);
        }
    }
}