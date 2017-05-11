using System.Security.Principal;
using Journalist;
using MGSUBackend.Authentification;
using UserManagment.Entities;

namespace MGSUCore.Authentification
{
    public class Principal : IPrincipal
    {
        private readonly UserRole _accountRole;

        public Principal(UserRole accountRole, IIdentity identity)
        {
            Require.NotNull(identity, nameof(identity));

            _accountRole = accountRole;
            Identity = identity;
        }

        public bool IsEmpty { get; private set; }

        public static IPrincipal EmptyPrincipal
            => new Principal(UserRole.User, Identitiy.EmptyIdentity) {IsEmpty = true};

        public bool IsInRole(string role)
        {
            return !IsEmpty && _accountRole.ToString("G").Equals(role);
        }

        public IIdentity Identity { get; }

        public bool IsInRole(UserRole role)
        {
            return (_accountRole == UserRole.Admin || _accountRole == role) && !IsEmpty;
        }
    }
}