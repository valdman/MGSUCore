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
    public class Repository<T> : IRepository<T> where T : PersistentEntity
    {
        private readonly IMongoCollection<T> _collection;

        public Repository(SessionProvider sessionProvider)
        {
            _collection = sessionProvider.GetCollection<T>();
        }

        public T GetById(ObjectId id)
        {
            Require.NotNull(id, nameof(id));

            var eq = Builders<T>.Filter.And(Builders<T>.Filter.Eq("Id", id), Builders<T>.Filter.Eq("IsDeleted", false));

            return _collection.Find(eq).SingleOrDefault();
        }

        public IEnumerable<T> GetByPredicate(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null
                ? _collection.AsQueryable()
                : _collection.AsQueryable().OfType<T>().Where(predicate).Where(_ => !_.IsDeleted);
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

            var eq = Builders<T>.Filter.And(Builders<T>.Filter.Eq("Id", @object.Id),
                Builders<T>.Filter.Eq("IsDeleted", false));

            _collection.ReplaceOne(eq, @object);
        }

        public void Delete(ObjectId id)
        {
            Require.NotNull(id, nameof(id));

            var objToDelete = GetById(id);

            objToDelete.IsDeleted = true;

            Update(objToDelete);
        }
    }
}