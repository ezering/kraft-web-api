using System.Text.RegularExpressions;
using Domain.Abstractions;
using Domain.Primitives;
using ErrorOr;

namespace Domain.ValueObjects.Users;

public sealed partial class Username : ValueObject, IValidateValueObject<Username>
{
    public string Value { get; }
    
    private const int AllowedUsernameMaxLength = 12;
    private const int AllowedUsernameMinLength = 4;

    private Username(string value)
    { 
        Value = value;
    }
    
    public static ErrorOr<Username> Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            return Errors.Errors.Username.Empty;
        }
        
        if(!UsernameRegex().IsMatch(value))
        {
            return Errors.Errors.Username.UsernameInvalidCharacters(value);
        }
        
        return value.Length switch
        {
            > AllowedUsernameMaxLength => Errors.Errors.Username.UsernameTooLong(AllowedUsernameMaxLength),
            < AllowedUsernameMinLength => Errors.Errors.Username.UsernameTooShort(AllowedUsernameMinLength),
            _ => new Username(value)
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    [GeneratedRegex("^[a-zA-Z0-9]*$")]
    private static partial Regex UsernameRegex();
    
    public ErrorOr<Username> ValidateValueObject()
    {
        if (string.IsNullOrWhiteSpace(Value))
        {
            return Errors.Errors.Username.Empty;
        }
        
        if (!UsernameRegex().IsMatch(Value))
        {
            return Errors.Errors.Username.UsernameInvalidCharacters(Value);
        }

        return Value.Length switch
        {
            > AllowedUsernameMaxLength => Errors.Errors.Username.UsernameTooLong(AllowedUsernameMaxLength),
            < AllowedUsernameMinLength => Errors.Errors.Username.UsernameTooShort(AllowedUsernameMinLength),
            _ => this
        };
    }
    
    
}