using ErrorOr;

namespace Domain.Errors;

public static partial class Errors
{
    public static class Username
    {
        // null or whitespace
        public static Error Empty => Error.Validation(
            code: "Username.Empty",
            description: "Username cannot be empty.");
        
        public static Error UsernameAlreadyExists(string username) =>
            Error.Conflict(code: "Username.UsernameAlreadyExists", description: $"Username {username} already exists");
        
        public static Error UsernameTooShort(int minLength) => Error.Validation(
            code: "Username.TooShort",
            description: $"Username must be at least {minLength} characters long.");
        
        public static Error UsernameTooLong(int maxLength) => Error.Validation(
            code: "Username.TooLong",
            description: $"Username must be at most {maxLength} characters long.");
        
        public static Error UsernameInvalidCharacters(string username) => Error.Validation(
            code: "Username.InvalidCharacters",
            description: $"Username must contain only letters, digits, and underscores. Username: {username}");
    }
    
}