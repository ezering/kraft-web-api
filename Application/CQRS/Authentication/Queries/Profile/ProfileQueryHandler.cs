using Application.CQRS.Authentication.Common;
using Domain.Abstractions;
using Domain.Errors;
using Domain.ValueObjects.Users;
using MediatR;
using ErrorOr;

namespace Application.CQRS.Authentication.Queries.Profile;

internal sealed class ProfileQueryHandler : IRequestHandler<ProfileQuery, ErrorOr<ProfileResponse>>
{
    private readonly IUserRepository _userRepository;

    public ProfileQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<ProfileResponse>> Handle(ProfileQuery request, CancellationToken cancellationToken)
    {
        var userId = UserId.Create(request.UserId);
        if (userId.IsError) return Errors.Authentication.WrongArguments;
        
        var user = await _userRepository.GetByIdAsync(userId.Value);
        if (user is null) return Errors.Authentication.UserNotFound;
        
        return new ProfileResponse(
            user.Id.Value.ToString(),
            user.Username.Value,
            user.Firstname.Value,
            user.Lastname.Value,
            user.Email.Value,
            user.ProfilePicture.Value);
    }
}