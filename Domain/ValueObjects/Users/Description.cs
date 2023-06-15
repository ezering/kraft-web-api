using System.Text.RegularExpressions;
using Domain.Primitives;
using ErrorOr;

namespace Domain.ValueObjects.Users;

public sealed partial class Description : ValueObject
{
    public string Value { get; }
    
    private const int AllowedDescriptionMaxLength = 1024;
    private const int AllowedDescriptionMinLength = 8;
    
    private Description(string value)
    {
        Value = value;
    }
    
    public static ErrorOr<Description> Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
            return new Description(string.Empty);
        
        if (!DescriptionRegex().IsMatch(value))
            return new Description(string.Empty);
        
        return value.Length switch 
        {
            > AllowedDescriptionMaxLength => new Description(value.Substring(0, AllowedDescriptionMaxLength)),
            < AllowedDescriptionMinLength => new Description(string.Empty),
            _ => new Description(value)
        };


    }
    
    [GeneratedRegex("^[A-Za-z0-9\\s.,!?()-]+$")]
    private static partial Regex DescriptionRegex();
    
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield  return Value;
    }
}