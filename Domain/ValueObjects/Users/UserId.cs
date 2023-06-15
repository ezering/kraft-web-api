using Domain.Abstractions;
using Domain.Exceptions;
using Domain.Primitives;
using ErrorOr;

namespace Domain.ValueObjects.Users;

public sealed class UserId: ValueObject, IValidateValueObject<UserId>
{
    public Guid Value { get; }

    private UserId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }
        Value = value;
    }
    
    public static ErrorOr<UserId> Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            return Errors.Errors.Guids.Empty;
        }
        
        if (value == default)
        {
            return Errors.Errors.Guids.Default;
        }

        return new UserId(value);
    }
    
    public static UserId CreateUnique() => new(Guid.NewGuid());
    
    protected override IEnumerable<object> GetEqualityComponents()   
    {
        yield return  Value;
    }

    public ErrorOr<UserId> ValidateValueObject()
    {
        if (Value == Guid.Empty)
            return Errors.Errors.Guids.Empty;
        
        if (Value == default)
            return Errors.Errors.Guids.Default;

        return this;
    }
}