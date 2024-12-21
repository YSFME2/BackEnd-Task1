using Domain.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : AuditableEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public bool IsChanged => _context.ChangeTracker.Entries<TEntity>().Any(x => x.State != EntityState.Unchanged && x.State != EntityState.Unchanged);
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);
        public void Delete(TEntity entity) => _dbSet.Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetFilteredAsync<TFilter>(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            var iQuerable = _dbSet.Where(filter);
            foreach (var include in includes)
                iQuerable = iQuerable.Include(include);

            return await _dbSet.ToListAsync();
        }
    }
}
