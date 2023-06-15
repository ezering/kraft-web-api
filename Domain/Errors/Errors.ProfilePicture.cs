using ErrorOr;

namespace Domain.Errors;

public static partial class Errors
{
    public static class ProfilePicture
    {
        public static Error Empty => Error.Validation(
            code: "ProfilePicture.Empty",
            description: "ProfilePicture cannot be empty.");
    }
}