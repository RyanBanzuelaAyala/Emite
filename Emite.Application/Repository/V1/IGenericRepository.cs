using System.Linq.Expressions;

namespace Emite.Application.Repository.V1
{
    public interface IGenericRepository<TEntity> where TEntity : class, new()
    {
        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(int id);

        TEntity GetBy(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> match);
    }
}
