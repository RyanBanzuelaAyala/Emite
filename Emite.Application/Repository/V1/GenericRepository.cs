using Emite.Data.V1;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Emite.Application.Repository.V1
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {
        private readonly EmiteDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(EmiteDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = GetBy(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        public TEntity GetBy(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return _dbSet.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return _context.Set<TEntity>().Where(match);
        }

    }
}
