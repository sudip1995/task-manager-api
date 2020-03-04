using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace TaskManager.Library.Database
{
    public interface IGenericRepository<TEntity> where TEntity : ARepositoryItem
    {
        void InsertOne(TEntity entity);
        TEntity Get(string id);
        List<TEntity> GetAll();
        void Update(string id, TEntity entity);
        List<TEntity> GetItemsByCondition(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> GetItemsByFilter(FilterDefinition<TEntity> filter);
    }
}