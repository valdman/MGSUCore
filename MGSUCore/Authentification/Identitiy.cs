using System.Security.Principal;
using MongoDB.Bson;

namespace MGSUBackend.Authentification
{
    public class Identitiy : IIdentity
    {
        public Identitiy(ObjectId userId, bool isAuthenticated)
        {
            UserId = userId;
            IsAuthenticated = isAuthenticated;
        }

        public ObjectId UserId { get; }

        public static Identitiy EmptyIdentity => new Identitiy(ObjectId.Empty, false);

        public string Name => UserId.ToString();

        public string AuthenticationType => "Token";

        public bool IsAuthenticated { get; }
    }
}