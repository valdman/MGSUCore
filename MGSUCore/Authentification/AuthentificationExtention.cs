using System;
using System.Security.Principal;
using MGSUCore.Authentification;
using MongoDB.Bson;
using UserManagment.Entities;

namespace MGSUBackend.Authentification
{
    public static class AuthorizationExtensions
    {
        public static bool IsInRole(this IPrincipal principal, UserRole role)
        {
            var ourPrincipal = principal as Principal;
            return ourPrincipal?.IsInRole(role) ?? false;
        }

        public static ObjectId GetId(this IIdentity identity)
        {
            var ourIdentity = identity as Identitiy;
            if (ourIdentity != null)
                return ourIdentity.UserId;

            throw new ArgumentException("Identity is not valid identity");
        }

        public static void AssertResourceOwnerOrAdmin(this IPrincipal principal, ObjectId identityId)
        {
            var ourPrincipal = principal as Principal;
            if (ourPrincipal?.Identity.GetId() != identityId && !principal.IsInRole(UserRole.Admin))
                throw new UnauthorizedAccessException();
        }

        public static void AssertResourceOwner(this IPrincipal principal, ObjectId identityId)
        {
            var ourPrincipal = principal as Principal;
            if (ourPrincipal?.Identity.GetId() != identityId)
                throw new UnauthorizedAccessException();
        }
    }
}