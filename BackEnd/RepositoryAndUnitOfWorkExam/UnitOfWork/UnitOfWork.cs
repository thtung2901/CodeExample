using Sts.Framework.Module.User.Models.Interfaces;
using Sts.Framework.Shared.Data;

namespace Sts.Framework.Module.User.Models.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly MainDbContext _context;
    private bool _disposed;

    public UnitOfWork(MainDbContext context, IUserPermissionRepository userPermissionRepository)
    {
        _context = context;
        UserPermissionRepository = userPermissionRepository;
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public IUserPermissionRepository UserPermissionRepository { get; }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}