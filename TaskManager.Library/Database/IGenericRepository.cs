using System.Collections.Generic;

namespace TaskManager.Library.Database
{
    public interface IGenericRepository<TEntity> where TEntity : ARepositoryItem
    {
        void InsertOne(TEntity entity);
        TEntity GetById(string id);
        List<TEntity> GetAll();
        TEntity Update(string id, TEntity entity);
    }
}