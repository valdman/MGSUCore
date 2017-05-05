using System;
using Common;
using MongoDB.Bson;

namespace UserManagment.Entities
{
    public class Session : PersistentEntity
    {
        public ObjectId UserId { get; set; }
        public Guid Sid { get; set; }
        public DateTimeOffset ExpireTime {get; set; }

        public const string CookieName = "sid";
    }
}