using Sts.Framework.Module.User.Models.Interfaces;
using Sts.Framework.Shared.Data;
using Sts.Framework.Shared.Data.Entities;

namespace Sts.Framework.Module.User.Models.Repositories;
public class UserPermissionRepository : BaseRepository<UserPermission>, IUserPermissionRepository
{
    public UserPermissionRepository(MainDbContext context) : base(context)
    {
        // Specific Customer logic
    }
}