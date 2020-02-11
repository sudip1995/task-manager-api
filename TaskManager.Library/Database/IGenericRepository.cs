using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TaskManager.Library.Database
{
    public interface IGenericRepository<TEntity> where TEntity : ARepositoryItem
    {
        void InsertOne(TEntity entity);
        TEntity GetById(string id);
        List<TEntity> GetAll();
        void Update(string id, TEntity entity);
        List<TEntity> GetItemsByCondition(Expression<Func<TEntity, bool>> predicate);
    }
}