using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mail;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using UserManagment.Entities;

namespace MGSUBackend.Tests
{
    [TestClass]
    public class DBConnectionTest
    {
        [TestInitialize]
        public void Setup()
        {
            _userIdsToDelete = new Stack<ObjectId>();
        }

        [TestMethod]
        public void ConnectToLocalDatabaseSucceed()
        {
            var sessionProvider = new SessionProvider("mongodb://mgsu:mgsuForJambul@127.0.0.1:27017/mgsu");

            Debug.WriteLine(sessionProvider.GetCollection<User>().ToJson());
        }

        [TestMethod]
        public void CreateGetAndDeleteUserHavingSameIds()
        {
            var sessionProvider = new SessionProvider("mongodb://mgsu:mgsuForJambul@127.0.0.1:27017/mgsu");

            var userRepo = new Repository<User>(sessionProvider);

            var usr = new User
            {
                Email = "boris@mail.ru",
                FirstName = "Boris",
                LastName = "Valdman"
            };

            var id = userRepo.Create(usr);
            var gettedUsr = userRepo.GetById(id);
            Assert.AreEqual(id, gettedUsr.Id);

            userRepo.Delete(id);
            gettedUsr = userRepo.GetById(id);
            Assert.IsNull(gettedUsr);
        }

        [TestMethod]
        [ExpectedException(typeof(MongoWriteException))]
        public void TestDoubleInsertFailedWithMongoWriteException()
        {
            var sessionProvider = new SessionProvider("mongodb://mgsu:mgsuForJambul@127.0.0.1:27017/mgsu");

            var userRepo = new Repository<User>(sessionProvider);

            var usr = new User
            {
                Email = "boris@mail.ru",
                FirstName = "Boris",
                LastName = "Valdman"
            };

            _userIdsToDelete.Push(userRepo.Create(usr));
            userRepo.Create(usr);
        }

        [TestCleanup]
        public void Cleanup()
        {
            var sessionProvider = new SessionProvider("mongodb://mgsu:mgsuForJambul@127.0.0.1:27017/mgsu");

            var userRepo = new Repository<User>(sessionProvider);

            while (_userIdsToDelete.Count > 0)
            {
                userRepo.Delete(_userIdsToDelete.Pop());
            }
        }

        private Stack<ObjectId> _userIdsToDelete;
    }
}
