using DataAccess.Application;
using Journalist;
using MongoDB.Driver;

namespace DataAccess
{
    public class SessionProvider : ISessionProvider
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public SessionProvider(string connectionString)
        {
            Require.NotEmpty(connectionString, nameof(connectionString));

            var connectionStringValidated = new MongoDB.Driver.Core.Configuration.ConnectionString(connectionString);

            _client = new MongoClient(connectionStringValidated.ToString());
            _database = _client.GetDatabase(connectionStringValidated.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }
    }
}