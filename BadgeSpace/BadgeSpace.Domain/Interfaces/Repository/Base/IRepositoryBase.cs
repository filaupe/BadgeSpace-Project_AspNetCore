using System.Linq.Expressions;

namespace BadgeSpace.Domain.Interfaces.Repository.Base
{
    public interface IRepositoryBase<TEntity, in TId> where TEntity : class where TId : struct
    {
        Task<TEntity> AddAsync(TEntity entity);

        Task<IEnumerable<TEntity>> AddListAsync(IEnumerable<TEntity> entity);

        void Remove(TEntity entidade);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where);

        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> where);

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> icludeProperties);
    }
}
