using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common;
using DataAccess.Application;
using Journalist;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : Identifyable
    {
        public T GetById(ObjectId id)
        {
            Require.NotNull(id, nameof(id));

            var eq = Builders<T>.Filter.Eq("Id", id);

            return _collection.Find(eq).SingleOrDefault();
        }

        public IEnumerable<T> GetByPredicate(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null
                ? _collection.AsQueryable().ToList()
                : _collection.AsQueryable().Where(predicate).ToList();
        }

        public ObjectId Create(T @object)
        {
            Require.NotNull(@object, nameof(@object));

            _collection.InsertOne(@object);

            return @object.Id;
        }

        public void Update(T @object)
        {
            Require.NotNull(@object, nameof(@object));

            var eq = Builders<T>.Filter.Eq("Id", @object.Id);
            
            _collection.ReplaceOne(eq, @object);

        }

        public void Delete(ObjectId id)
        {
            Require.NotNull(id, nameof(id));

            var eq = Builders<T>.Filter.Eq("Id", id);

            _collection.DeleteOne(eq);
        }

        private readonly IMongoCollection<T> _collection;

        public Repository(SessionProvider sessionProvider)
        {
            _collection = sessionProvider.GetCollection<T>();
        }
    }
}