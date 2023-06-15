using FluentValidation;

namespace Application.CQRS.Authentication.Queries.Profile;

public class ProfileQueryValidator : AbstractValidator<ProfileQuery>
{
    public ProfileQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}