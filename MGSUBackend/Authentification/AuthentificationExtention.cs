using System;
using System.Security.Principal;
using MongoDB.Bson;
using UserManagment.Entities;

namespace MGSUBackend.Authentification
{
    public static class AuthorizationExtensions
    {
        public static bool IsInRole(this IPrincipal principal, UserRole role)
        {
            var lodPrincipal = principal as Principal;
            return lodPrincipal?.IsInRole(role) ?? false;
        }

        public static ObjectId GetId(this IIdentity identity)
        {
            var lodIdentity = identity as Identitiy;
            if (lodIdentity != null)
                return lodIdentity.UserId;

            throw new ArgumentException("Identity is not valid identity");
        }

        public static void AssertResourceOwnerOrAdmin(this IPrincipal principal, ObjectId identityId)
        {
            var lodPrincipal = principal as Principal;
            if (lodPrincipal?.Identity.GetId() != identityId && !principal.IsInRole(UserRole.Admin))
                throw new UnauthorizedAccessException();
        }

        public static void AssertResourceOwner(this IPrincipal principal, ObjectId identityId)
        {
            var lodPrincipal = principal as Principal;
            if (lodPrincipal?.Identity.GetId() != identityId)
                throw new UnauthorizedAccessException();
        }
    }
}