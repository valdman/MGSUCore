using System;
using System.Linq;
using DataAccess.Application;
using Journalist;
using MongoDB.Bson;
using UserManagment.Application;
using UserManagment.Entities;

namespace UserManagment
{
    public class SessionManager : ISessionManager
    {
        private readonly IRepository<Session> _sessionRepository;

        public SessionManager(IRepository<Session> sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public Session GetSessionForUser(ObjectId userId)
        {
            Require.NotNull(userId, nameof(userId));

            var currentSession = _sessionRepository.GetByPredicate(sesh => sesh.UserId == userId).SingleOrDefault();

            if (currentSession != null && currentSession.ExpireTime > DateTimeOffset.UtcNow) return currentSession;

            currentSession = new Session
            {
                CreatingTime = BsonDateTime.Create(DateTimeOffset.Now),
                ExpireTime = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(14)),
                Sid = Guid.NewGuid(),
                UserId = userId
            };

            _sessionRepository.Create(currentSession);

            return currentSession;
        }

        public void EndSessionForUser(ObjectId userId)
        {
            Require.NotNull(userId, nameof(userId));

            var currentSession = GetSessionForUser(userId);

            if (currentSession == null) return;

            _sessionRepository.Delete(currentSession.Id);
        }

        public void EndSessionbyId(Guid sessionId)
        {
            Require.NotNull(sessionId, nameof(sessionId));

            var session = GetSessionBySid(sessionId);

            _sessionRepository.Delete(session?.Id ?? ObjectId.Empty);
        }

        public ObjectId GetUserIdBySessionId(Guid sessionId)
        {
            Require.NotNull(sessionId, nameof(sessionId));

            return GetSessionBySid(sessionId)
                       ?.UserId ?? ObjectId.Empty;
        }

        public Session GetSessionBySid(Guid sid)
        {
            Require.NotNull(sid, nameof(sid));

            return _sessionRepository.GetByPredicate(sesh => sesh.Sid == sid).FirstOrDefault();
        }
    }
}