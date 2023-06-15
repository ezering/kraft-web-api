using ErrorOr;

namespace Domain.Errors;

public static partial class Errors
{
    public static class Roles
    {
        public static Error RoleNotFound(string name) => Error.Validation(
            code: "Role.NotFound",
            description: $"Role with name {name} not found.");

        public static Error RoleCannotBeEmpty => Error.Validation( 
            code: "Role.Empty",
            description: "Role cannot be empty.");
        
        public static Error RoleTooLong(int maxLength) => Error.Validation(
            code: "Role.TooLong",
            description: $"Role cannot be longer than {maxLength} characters.");
        
        public static Error RoleTooShort(int minLength) => Error.Validation(
            code: "Role.TooShort",
            description: $"Role cannot be shorter than {minLength} characters.");
    }   
}