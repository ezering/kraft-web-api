using Domain.Abstractions;
using Domain.Primitives;
using ErrorOr;

namespace Domain.ValueObjects.Users;

public sealed class Password: ValueObject, IValidateValueObject<Password>
{
    public string Value { get; }
    
    private const int AllowedPasswordMaxLength = 1024;
    private const int AllowedPasswordMinLength = 8;

    private Password(string value)
    {
        Value = value;
    }
    
    public static ErrorOr<Password> Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            return Errors.Errors.Password.Empty;
        }
        
        
        return value.Length switch
        {
            > AllowedPasswordMaxLength => Errors.Errors.Password.TooLong(AllowedPasswordMaxLength),
            < AllowedPasswordMinLength => Errors.Errors.Password.TooShort(AllowedPasswordMinLength),
            _ => new Password(value)
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public ErrorOr<Password> ValidateValueObject()
    {
        if (string.IsNullOrWhiteSpace(Value))
        {
            return Errors.Errors.Password.Empty;
        }

        return Value.Length switch
        {
            > AllowedPasswordMaxLength => Errors.Errors.Password.TooLong(AllowedPasswordMaxLength),
            < AllowedPasswordMinLength => Errors.Errors.Password.TooShort(AllowedPasswordMinLength),
            _ => this
        };
    }
}