using Domain.Abstractions;
using Domain.Primitives;
using ErrorOr;

namespace Domain.ValueObjects.Users;

public sealed class ProfilePicture : ValueObject, IValidateValueObject<ProfilePicture>
{
    public string Value { get; }

    private ProfilePicture(string value)
    {
        Value = value;
    }
    
    public static ErrorOr<ProfilePicture> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Errors.Errors.ProfilePicture.Empty;
        }

        return new ProfilePicture(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public ErrorOr<ProfilePicture> ValidateValueObject()
    {
        if (string.IsNullOrWhiteSpace(Value))
        {
            return Errors.Errors.ProfilePicture.Empty;
        }

        return this;
    }
}