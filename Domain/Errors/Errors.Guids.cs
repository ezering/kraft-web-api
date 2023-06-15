using ErrorOr;

namespace Domain.Errors;

public static partial class Errors
{
    public static class Guids
    {
        public static Error Default => Error.Validation(
            code: "Guid.Default",
            description: "Guid cannot be default.");

        public static Error Empty => Error.Validation(
            code: "Guid.Empty",
            description: "Guid cannot be empty.");
        
        public static Error InvalidFormat(string guid) => Error.Validation(
            code: "Guid.InvalidFormat",
            description: $"Guid {guid} is not in a valid format.");
    }
}