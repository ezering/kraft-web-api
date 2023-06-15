using Application.Auth;
using Application.CQRS.Authentication.Commands.Register;
using Application.CQRS.Authentication.Common;
using Application.CQRS.Authentication.Queries.Login;
using Application.CQRS.Authentication.Queries.Profile;
using Domain.Errors;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.V1;

[Route("auth")]
public sealed class AuthenticationController : ApiController
{
    private readonly IMapper _mapper;

    public AuthenticationController(IMapper mapper, IAuthenticator authenticator, ITokenStorage tokenStorage)
    {
        _mapper = mapper;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var authResult = await Sender.Send(command, cancellationToken);
        

        return authResult.Match(
            authenticationResult => Ok(_mapper.Map<AuthenticationResponse>(authenticationResult)),
            errors => Problem(errors)
            );
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var authResult = await Sender.Send(query, cancellationToken);
        
        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: authResult.FirstError.Description);
        }
        
        return authResult.Match(
            authenticationResult => Ok(_mapper.Map<AuthenticationResponse>(authenticationResult)),
            errors => Problem(errors));
    }
    
    [Authorize(Policy = "is-admin-or-user")]
    [HttpGet("profile")]
    public async Task<IActionResult> Profile(CancellationToken cancellationToken)
    {
        var roles = User.Claims.Where(x => x.Type == "role").Select(x => x.Value).ToList();
        if (!roles.Any())
            return Problem(
                type: "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                statusCode: StatusCodes.Status401Unauthorized,
                title: Errors.Authentication.WrongArguments.Description,
                instance: Request.Path,
                detail: "User is not authenticated");
        if (string.IsNullOrEmpty(User.Identity?.Name))
            return Problem(
                type: "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                statusCode: StatusCodes.Status401Unauthorized,
                title: Errors.Authentication.WrongArguments.Description,
                instance: Request.Path,
                detail: "User is not authenticated");
        
        var userId = Guid.Parse(User.Identity.Name);
        var query = new ProfileQuery(userId);
        var profile = await Sender.Send(query, cancellationToken);
        
        if (profile.IsError && profile.FirstError == 
            Errors.Authentication.WrongArguments || profile.FirstError == Errors.Authentication.UserNotFound)
        {
            return Problem(
                type: "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                statusCode: StatusCodes.Status404NotFound,
                title: Errors.Authentication.WrongArguments.Description,
                instance: Request.Path,
                detail: "User is not authenticated or not found");
        }
        
        return profile.Match(Ok, Problem);
    }
}