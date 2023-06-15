using Domain.Abstractions;
using Domain.Primitives;
using ErrorOr;

namespace Domain.ValueObjects.Users;

public sealed class Firstname: ValueObject, IValidateValueObject<Firstname>
{
    public string Value { get; }

    private const int AllowedFirstnameMaxLength = 50;
    private const int AllowedFirstnameMinLength = 2;

    private Firstname(string value)
    {
      Value = value;
    }
    
    public static ErrorOr<Firstname> Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            return Errors.Errors.Firstname.Empty;
        }
        
        return value.Length switch
        {
            > AllowedFirstnameMaxLength => Errors.Errors.Firstname.FirstnameTooLong(AllowedFirstnameMaxLength),
            < AllowedFirstnameMinLength => Errors.Errors.Firstname.FirstnameTooShort(AllowedFirstnameMinLength),
            _ => new Firstname(value)
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public ErrorOr<Firstname> ValidateValueObject()
    {
        if(string.IsNullOrWhiteSpace(Value))
        {
            return Errors.Errors.Firstname.Empty;
        }
        
        return Value.Length switch
        {
            > AllowedFirstnameMaxLength => Errors.Errors.Firstname.FirstnameTooLong(AllowedFirstnameMaxLength),
            < AllowedFirstnameMinLength => Errors.Errors.Firstname.FirstnameTooShort(AllowedFirstnameMinLength),
            _ => this
        };
    }
}