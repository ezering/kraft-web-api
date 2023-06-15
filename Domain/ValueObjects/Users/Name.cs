using System.Text.RegularExpressions;
using Domain.Primitives;
using ErrorOr;

namespace Domain.ValueObjects.Users;

public sealed partial class Name : ValueObject
{
    public string Value { get; }
    
    private const int AllowedNameMaxLength = 15;
    private const int AllowedNameMinLength = 2;
    
    private Name(string value)
    {
        Value = value;
    }
    
    public static ErrorOr<Name> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.Errors.Name.Empty;
        
        if (!NameRegex().IsMatch(value))
            return Errors.Errors.Name.NameInvalidCharacters(value);

        return value.Length switch 
        {
            > AllowedNameMaxLength => Errors.Errors.Name.NameTooLong(AllowedNameMaxLength),
            < AllowedNameMinLength => Errors.Errors.Name.NameTooShort(AllowedNameMinLength),
            _ => new Name(value)
        };
    }
    
    [GeneratedRegex("^[A-Za-z-_]+$")]
    private static partial Regex NameRegex();
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}