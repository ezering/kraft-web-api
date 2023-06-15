using ErrorOr;

namespace Domain.Errors;

public static partial class Errors
{
    public static class Firstname
    {
        public static Error Empty => Error.Validation(
            code: "Firstname.Empty",
            description: "Firstname cannot be empty.");
        
        public static Error FirstnameTooShort(int minLength) => Error.Validation(
            code: "Firstname.TooShort",
            description: $"Firstname must be at least {minLength} characters long.");
        
        public static Error FirstnameTooLong(int maxLength) => Error.Validation(
            code: "Firstname.TooLong",
            description: $"Firstname must be at most {maxLength} characters long.");
        
        public static Error FirstnameInvalidCharacters(string firstname) => Error.Validation(
            code: "Firstname.InvalidCharacters",
            description: $"Firstname must contain only letters. Firstname: {firstname}");
        
    }
}