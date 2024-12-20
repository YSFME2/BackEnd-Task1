using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : AuditableEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetFilteredAsync<TFilter>(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);

        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
    }
}
