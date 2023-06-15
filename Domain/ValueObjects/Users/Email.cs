using System.Text.RegularExpressions;
using Domain.Abstractions;
using Domain.Primitives;
using ErrorOr;

namespace Domain.ValueObjects.Users;

public sealed partial class Email : ValueObject, IValidateValueObject<Email>
{
    public string Value { get; }
    
    private const int AllowedEmailMaxLength = 50;
    private const int AllowedEmailMinLength = 2;
    
    private Email(string value)
    {
       Value = value;
    }
    
    public static ErrorOr<Email> Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            return Errors.Errors.Email.Empty;
        }
        
        if(!EmailRegex().IsMatch(value))
        {
            return Errors.Errors.Email.EmailInvalidCharacters(value);
        }
        
        return value.Length switch
        {
            > AllowedEmailMaxLength => Errors.Errors.Email.EmailTooLong(AllowedEmailMaxLength),
            < AllowedEmailMinLength => Errors.Errors.Email.EmailTooShort(AllowedEmailMinLength),
            _ => new Email(value)
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    [GeneratedRegex("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$")]
    private static partial Regex EmailRegex();

    public ErrorOr<Email> ValidateValueObject()
    {
        if (string.IsNullOrWhiteSpace(Value))
        {
            return Errors.Errors.Email.Empty;
        }
        if (!EmailRegex().IsMatch(Value))
        {
            return Errors.Errors.Email.EmailInvalidCharacters(Value);
        }

        return Value.Length switch
        {
            > AllowedEmailMaxLength => Errors.Errors.Email.EmailTooLong(AllowedEmailMaxLength),
            < AllowedEmailMinLength => Errors.Errors.Email.EmailTooShort(AllowedEmailMinLength),
            _ => this
        };
    }
}