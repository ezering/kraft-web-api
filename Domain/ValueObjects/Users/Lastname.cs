using Domain.Abstractions;
using Domain.Primitives;
using ErrorOr;

namespace Domain.ValueObjects.Users;

public sealed class Lastname: ValueObject, IValidateValueObject<Lastname>
{
    public string Value { get; }
    
    private const int AllowedLastnameMaxLength = 50;
    private const int AllowedLastnameMinLength = 2;
    
    private Lastname(string value)
    {
        Value = value;
    }
    
    public static ErrorOr<Lastname> Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            return Errors.Errors.Lastname.Empty;
        }
        
        return value.Length switch
        {
            > AllowedLastnameMaxLength => Errors.Errors.Lastname.LastnameTooLong(AllowedLastnameMaxLength),
            < AllowedLastnameMinLength => Errors.Errors.Lastname.LastnameTooShort(AllowedLastnameMinLength),
            _ => new Lastname(value)
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public ErrorOr<Lastname> ValidateValueObject()
    {
        if (string.IsNullOrWhiteSpace(Value))
        {
            return Errors.Errors.Lastname.Empty;
        }
        
        return Value.Length switch
        {
            > AllowedLastnameMaxLength => Errors.Errors.Lastname.LastnameTooLong(AllowedLastnameMaxLength),
            < AllowedLastnameMinLength => Errors.Errors.Lastname.LastnameTooShort(AllowedLastnameMinLength),
            _ => this
        };
    }
}