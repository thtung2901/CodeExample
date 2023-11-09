namespace Sts.Framework.Module.User.Models.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserPermissionRepository UserPermissionRepository { get; }
    Task<int> SaveChangesAsync();
}