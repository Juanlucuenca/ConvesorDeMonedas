using System.Linq.Expressions;

namespace ConvesorDeMonedas.Interfaces
{
    // Only type class in TEntity Generic.
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(object id);
        bool Insert(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(object id);
        bool Exist(object id);
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        bool Save();
    }
}
