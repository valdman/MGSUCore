using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
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
    }
}