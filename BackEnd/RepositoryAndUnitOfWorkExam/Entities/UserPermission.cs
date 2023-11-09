namespace Sts.Framework.Shared.Data.Entities;

public class UserPermission : EntityBase
{
    public Guid UserId { get; set; }

    public Guid ModulePermissionId { get; set; }

    public virtual ModulePermission ModulePermission { get; set; }
    public virtual User User { get; set; }
}
