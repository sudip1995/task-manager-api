using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using TaskManager.Library.Extensions;
using TaskManager.Library.Helpers;

namespace TaskManager.Library.Database
{
    [Export(typeof(IGenericRepository<>))]
    public class MongoDbRepository<TEntity> : IGenericRepository<TEntity> where TEntity : ARepositoryItem
    {
        private static IMongoClient _mongoClient;
        private IMongoDatabase _mongodb;
        private IMongoCollection<TEntity> _collection;
        private static readonly object LockObject = new object();
        public MongoDbRepository()
        {
            GetDatabase();
            GetCollection();
        }

        private void GetDatabase()
        {
            if (_mongoClient == null)
            {
                lock (LockObject)
                {
                    if (_mongoClient == null)
                    {
                        _mongoClient = new MongoClient(ConfigurationHelper.Instance.GetDatabaseConnectionString());
                    }
                }
            }
            _mongodb = _mongoClient.GetDatabase(ConfigurationHelper.Instance.GetDatabaseName());
        }

        private void GetCollection()
        {
            _collection = _mongodb.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public void InsertOne(TEntity entity)
        {
            _collection.InsertOne(entity);
        }

        public TEntity Get(string id)
        {
            return _collection.Find(o => o.Id == id).FirstOrDefault();
        }

        public List<TEntity> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public void Update(string id, TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(o => o.Id, id);

            _collection.ReplaceOne(filter, entity);
        }

        public List<TEntity> GetItemsByCondition(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection
                .AsQueryable<TEntity>()
                .Where(predicate.Compile())
                .ToList();
        }

        public List<TEntity> GetItemsByFilter(FilterDefinition<TEntity> filter)
        {
            return _collection.Find(filter).ToList();
        }

        public void UpdateOneByFilter(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            _collection.UpdateOne(filter, update);
        }
    }
}