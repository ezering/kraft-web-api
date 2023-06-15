using ErrorOr;

namespace Domain.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            code: "Auth.InvalidCred",
            description: "Invalid credentials.");

        public static Error UserNotFound => Error.Validation(
            code: "Auth.UserNotFound",
            description: "User not found.");
        
        public static Error WrongArguments => Error.Validation(
            code: "Auth.WrongArguments",
            description: $"You provided wrong arguments.");

        public static Error EmailAlreadyExists(string email) => Error.Validation(
            code: "Auth.EmailAlreadyExists",
            description: $"Email {email} already exists.");

        public static Error RoleDoesNotExist(string valueValue) => Error.Validation(
            code: "Auth.RoleDoesNotExist",
            description: $"Role {valueValue} does not exist.");
    }
}