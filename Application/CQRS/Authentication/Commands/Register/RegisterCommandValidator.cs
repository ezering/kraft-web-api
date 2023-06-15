using FluentValidation;

namespace Application.CQRS.Authentication.Commands.Register;

internal sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty();

        RuleFor(x => x.FirstName).NotEmpty();

        RuleFor(x => x.LastName).NotEmpty();

        RuleFor(x => x.Email).NotEmpty();

        RuleFor(x => x.Password).NotEmpty();

        RuleFor(x => x.ProfilePictureUrl).NotEmpty();
    }
}