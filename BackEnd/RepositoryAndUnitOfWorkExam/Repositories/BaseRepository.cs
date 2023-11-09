using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sts.Framework.Module.User.Models.Interfaces;
using Sts.Framework.Shared.Data;
using Sts.Framework.Shared.Data.Entities;

namespace Sts.Framework.Module.User.Models.Repositories;
public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
{
    private readonly MainDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(MainDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<Tuple<IEnumerable<TEntity>, int>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "",
        int? page = null, int? pageSize = null
        )
    {
        IQueryable<TEntity> query = _dbSet;

        // Apply the filter expression if it's not null
        if (filter != null)
        {
            query = query.Where(filter);
        }

        // Apply the include properties if they're not null or empty
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProperty in includeProperties.Split
                         (new [] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        // Apply the order by expression if it's not null
        if (orderBy != null)
        {
            query = orderBy(query);
        }

        var totalCount = query.Count();

        // Apply the pagination
        if (page != null && pageSize != null)
        {
            query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }

        // Get entities
        var entities = await query.ToListAsync();

        // Execute the query and return the result as a list
        return new Tuple<IEnumerable<TEntity>, int>(entities, totalCount);
    }

    public virtual async Task<Guid> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity.Id;
    }

    public virtual async Task<IEnumerable<Guid>> AddRangeAsync(IList<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        return entities.Select(x => x.Id);
    }

    public virtual void Update(TEntity entity)
    {
	    _context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Delete(TEntity entity)
    {
	    _dbSet.Remove(entity);
    }

    public virtual IEnumerable<Guid> RemoveRange(IList<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
        return entities.Select(x => x.Id);
    }
}