namespace Sts.Framework.Shared.Data.Entities;

public interface IEntity
{
    Guid Id { get; set; }
}

public interface IAuditable
{
    Guid? CreatedBy { get; set; }
    DateTime CreatedDate { get; set; }

    Guid? ModifiedBy { get; set; }
    DateTime? ModifiedDate { get; set; }
}

public abstract class EntityBase : IEntity, IAuditable
{
    public Guid Id { get; set; }

    public Guid? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }

    public bool IsDeleted { get; set; }

    protected EntityBase()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
    }
}