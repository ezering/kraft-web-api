using ErrorOr;

namespace Domain.Errors;

public static partial class Errors
{
    public static class UserId
    {
        public static Error Empty => Error.Validation(
            code: "UserId.Empty",
            description: "UserId cannot be empty.");
        
        public static Error NotFound(Guid id) =>
            Error.NotFound(code: "UserId.NotFound", description: $"User with id {id} not found");
    }
}