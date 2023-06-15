using ErrorOr;

namespace Domain.Errors;

public static partial class Errors
{
    public static class Description
    {
        public static Error Empty => Error.Validation(
            code: "Description.Empty",
            description: "Description cannot be empty.");
        
        public static Error DescriptionTooShort(int min) =>
            Error.Validation(
                code: "Description.TooShort",
                description: $"Description is too short. It must be at least {min} characters long.");
        
        public static Error DescriptionTooLong(int max) =>
            Error.Validation(
                code: "Description.TooLong",
                description: $"Description is too long. It must be at most {max} characters long.");
        
        public static Error DescriptionInvalidCharacters(string description) =>
            Error.Validation(
                code: "Description.InvalidCharacters",
                description: $"Description {description} contains invalid characters. It must contain only letters.");
    }
}