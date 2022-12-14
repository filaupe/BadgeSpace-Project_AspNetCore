using BadgeSpace.Domain.Entities.Base;
using BadgeSpace.Domain.Interfaces.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace BadgeSpace.Infra.Repositories.Entities.Base
{
    public class RepositoryBase<TEntity, TId> : IRepositoryBase<TEntity, TId>
        where TEntity : EntityBaseModel
        where TId : struct
    {
        private readonly ApplicationDbContext _context;

        public RepositoryBase(ApplicationDbContext context) => _context = context;

        public async Task<TEntity> AddAsync(TEntity entity)
            => (await _context.Set<TEntity>().AddAsync(entity)).Entity;

        public async Task<IEnumerable<TEntity>> AddListAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where) 
            => await _context.Set<TEntity>().AnyAsync(where);

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> where)
            => await _context.Set<TEntity>().FirstOrDefaultAsync(where);

        public void Remove(TEntity entity) => _context.Set<TEntity>().Remove(entity);

        public IQueryable<TEntity> ToListAsync(Expression<Func<TEntity, bool>>? icludeProperties)
            => icludeProperties == null ? _context.Set<TEntity>() : _context.Set<TEntity>().Where(icludeProperties);
    }
}