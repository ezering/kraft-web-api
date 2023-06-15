using Application.CQRS.Users.Common;
using Domain.Abstractions;
using Domain.Errors;
using Domain.ValueObjects.Users;
using MediatR;
using ErrorOr;
namespace Application.CQRS.Users.Queries.GetUser;

internal sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, ErrorOr<GetUserResponse>>
{
    private readonly IUserRepository _userRepository;
    
    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<ErrorOr<GetUserResponse>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        
        var userId = UserId.Create(query.Id);
        if (userId.IsError) return userId.Errors;
        
        var user = await _userRepository.GetByIdAsync(userId.Value);
        if (user is null) return Errors.Users.UserNotFound;
        
        return new GetUserResponse(
            user.Id.Value,
            user.Username.Value,
            user.Firstname.Value,
            user.Lastname.Value,
            user.Email.Value,
            user.ProfilePicture.Value);
    }
}