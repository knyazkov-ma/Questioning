using Questioning.Entity;
using System.Collections.Generic;

namespace Questioning.Repository.Interface
{
    public interface IRepository<TEntity>
        where TEntity: BaseEntity
    {
        TEntity Get(string id);
        IEnumerable<TEntity> GetList();
        void Save(TEntity entity);
        void Delete(string id);
    }
}
