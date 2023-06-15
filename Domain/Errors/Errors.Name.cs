using ErrorOr;

namespace Domain.Errors;

public static partial class Errors
{
    public static class Name
    {
        public static Error Empty => Error.Validation(
            code: "Name.Empty",
            description: "Name cannot be empty.");
        
        public static Error NameTooShort(int min) =>
            Error.Validation(
                code: "Name.TooShort",
                description: $"Name is too short. It must be at least {min} characters long.");
        
        public static Error NameTooLong(int max) =>
            Error.Validation(
                code: "Name.TooLong",
                description: $"Name is too long. It must be at most {max} characters long.");
        
        public static Error NameInvalidCharacters(string name) =>
            Error.Validation(
                code: "Name.InvalidCharacters",
                description: $"Name {name} contains invalid characters. It must contain only letters.");
    }
}