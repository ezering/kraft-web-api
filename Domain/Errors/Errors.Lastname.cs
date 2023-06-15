using ErrorOr;

namespace Domain.Errors;

public static partial class Errors
{
    public static class Lastname
    {
        public static Error Empty => Error.Validation(
            code: "Lastname.Empty",
            description: "Lastname cannot be empty.");
        
        public static Error LastnameTooShort(int min) =>
            Error.Validation(
                code: "Lastname.TooShort",
                description: $"Lastname is too short. It must be at least {min} characters long.");

        public static Error LastnameTooLong(int max) =>
            Error.Validation(
                code: "Lastname.TooLong",
                description: $"Lastname is too long. It must be at most {max} characters long.");
        
        public static Error LastnameInvalidCharacters(string lastname) =>
            Error.Validation(
                code: "Lastname.InvalidCharacters",
                description: $"Lastname {lastname} contains invalid characters. It must contain only letters.");
    }
}