using Application.Auth;
using Application.CQRS.Authentication.Common;
using Application.Security;
using Domain.Abstractions;
using Domain.Errors;
using Domain.ValueObjects.Users;
using ErrorOr;
using MediatR;

namespace Application.CQRS.Authentication.Queries.Login;

internal sealed class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticator _authenticator;
    private readonly IPasswordManager _passwordManager;
    private readonly ITokenStorage _tokenStorage;


    public LoginQueryHandler(
        IUserRepository userRepository, 
        IAuthenticator authenticator, 
        IPasswordManager passwordManager,
        ITokenStorage tokenStorage)
    {
        _userRepository = userRepository;
        _authenticator = authenticator;
        _passwordManager = passwordManager;
        _tokenStorage = tokenStorage;
    }
    
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var email = Email.Create(query.Email);
        if (email.IsError) return email.Errors;
        
        var user = await _userRepository.GetUserByEmailAsync(email.Value);
        if (user is null) return Errors.Authentication.InvalidCredentials;
        
        var password = Password.Create(query.Password);
        if (password.IsError) return Errors.Authentication.InvalidCredentials;

        if(!_passwordManager.Validate(password.Value.Value, user.Password.Value))
            return Errors.Authentication.InvalidCredentials;
        
        var jwtToken = _authenticator.CreateToken(user.Id.Value, user.Roles.Select(x => x.Value).ToList());
        _tokenStorage.Set(jwtToken);
        
        return new AuthenticationResult(
            user,
            AccessToken: jwtToken.AccessToken);
    }
}