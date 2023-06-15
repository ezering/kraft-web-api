using ErrorOr;

namespace Domain.Errors;

public static partial class Errors
{
    public static class Users
    {
        public static Error UserNotFound => Error.Validation(
            code: "User.NotFound",
            description: "User not found.");
        
    }
}