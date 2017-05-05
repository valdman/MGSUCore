using System;
using MongoDB.Bson;
using UserManagment.Entities;

namespace UserManagment.Application
{
    public interface ISessionManager
    {
        Session GetSessionForUser(ObjectId userId);
        void EndSessionForUser(ObjectId userId);
        void EndSessionbyId(Guid sessionId);

        ObjectId GetUserIdBySessionId(Guid sessionId);
    }
}