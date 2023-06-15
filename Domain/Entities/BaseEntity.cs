using Domain.Primitives;

namespace Domain.Entities;

public abstract class BaseEntity<TId> : Entity<TId>
    where TId : notnull
{
    public DateTime DateCreated { get; private set; }
    public DateTime? DateUpdated { get; private set; }
    public DateTime? DateDeleted { get; private set; }
    
    protected BaseEntity(TId id, DateTime dateCreated, DateTime? dateUpdated, DateTime? dateDeleted) : base(id)
    {
        DateCreated = dateCreated;
        DateUpdated = dateUpdated;
        DateDeleted = dateDeleted;
    }
}