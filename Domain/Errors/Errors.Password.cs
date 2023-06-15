using ErrorOr;

namespace Domain.Errors;

public static partial class Errors
{
    public static class Password
    {
        public static Error Empty => Error.Validation(
            code: "Password.Empty",
            description: "Password cannot be empty.");
        
        public static Error NullOrWhiteSpace => Error.Validation(
            code: "Password.NullOrWhiteSpace",
            description: "Password cannot be null or whitespace.");
        
        public static Error TooShort(int minLength) => Error.Validation(
            code: "Password.TooShort",
            description: $"Password must be at least {minLength} characters long.");
        
        public static Error TooLong(int maxLength) => Error.Validation(
            code: "Password.TooLong",
            description: $"Password must be at most {maxLength} characters long.");

    }
}