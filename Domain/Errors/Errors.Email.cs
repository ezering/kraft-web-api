using ErrorOr;

namespace Domain.Errors;

public static partial class Errors
{
    public static class Email
    {
        public static Error Invalid(string email) =>
            Error.Validation(code: "Email.Invalid", description: $"Email {email} is invalid");

        public static Error Empty => Error.Validation(
            code: "Email.Empty",
            description: "Email cannot be empty.");
        
        public static Error EmailTooLong(int max) =>
            Error.Validation(
                code: "Email.TooLong",
                description: $"Email is too long. It must be at most {max} characters long.");
        
        public static Error EmailTooShort(int min) =>
            Error.Validation(
                code: "Email.TooShort",
                description: $"Email is too short. It must be at least {min} characters long.");
        
        public static Error EmailInvalidCharacters(string email) =>
            Error.Validation(
                code: "Email.InvalidCharacters",
                description: $"Email {email} contains invalid characters. It must contain only letters.");
    }
}