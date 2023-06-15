using Domain.Exceptions;
using Domain.Primitives;
using ErrorOr;

namespace Domain.ValueObjects.Users;

public sealed class Role : ValueObject 
{ 
    public static IEnumerable<string> AvailableRoles { get; } = new[] {"admin", "user"};

    public string Value { get; }
    
    private const int AllowedRoleMaxLength = 30;
    private const int AllowedRoleMinLength = 3;

    private Role(string value)
    {
        Value = value;
    }

    public static Role Admin() => new("admin");
    
    public static Role User() => new("user");

    public static implicit operator Role(string value) => new Role(value);

    public static implicit operator string(Role value) => value?.Value;

    public override string ToString() => Value;

    public static ErrorOr<Role> Create(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            return Errors.Errors.Roles.RoleCannotBeEmpty;

        if (!AvailableRoles.Contains(role))
            return Errors.Errors.Roles.RoleNotFound(role);
        
        return role.Length switch
        {
            > AllowedRoleMaxLength => Errors.Errors.Roles.RoleTooLong(AllowedRoleMaxLength),
            < AllowedRoleMinLength => Errors.Errors.Roles.RoleTooShort(AllowedRoleMinLength),
            _ => new Role(role)
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}