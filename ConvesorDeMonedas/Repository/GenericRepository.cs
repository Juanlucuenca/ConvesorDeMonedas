using ConvesorDeMonedas.Data;
using ConvesorDeMonedas.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ConvesorDeMonedas.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly Context _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _dbSet = context.Set<TEntity>();
        }

        public bool Exist(object id)
        {
            return _dbSet.Find(id) != null;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public bool Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            return Save();
        }

        public bool Update(TEntity entity)
        {
            _context.Update(entity);
            return Save();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public bool Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            if (entityToDelete != null)
            {
                _dbSet.Remove(entityToDelete);
                return Save();
            }
            return false;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
