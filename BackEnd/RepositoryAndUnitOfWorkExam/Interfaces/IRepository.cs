using Sts.Framework.Shared.Data.Entities;
using System.Linq.Expressions;

namespace Sts.Framework.Module.User.Models.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<Tuple<IEnumerable<T>, int>> GetAllAsync(Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int? page = null,
        int? pageSize = null);
    Task<Guid> AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<IEnumerable<Guid>> AddRangeAsync(IList<T> entities);
    IEnumerable<Guid> RemoveRange(IList<T> entities);
}