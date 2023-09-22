using System.Linq.Expressions;

namespace Assessment3.Server.Application.Common.Persistence;

public interface IRepository <TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "");

    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null);
    Task<TEntity?> GetByIdAsync(object id);

    Task InsertAsync(TEntity entity);

    Task DeleteAsync(object id);

    Task DeleteAsync(TEntity entityToDelete);

    Task UpdateAsync(TEntity entityToUpdate);
}