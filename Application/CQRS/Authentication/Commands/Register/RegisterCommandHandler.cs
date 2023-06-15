using Application.CQRS.Authentication.Common;
using Application.Security;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Errors;
using Domain.ValueObjects.Users;
using ErrorOr;
using MediatR;

namespace Application.CQRS.Authentication.Commands.Register;

internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{ 
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IUnitOfWork _unitOfWork;
    
    public RegisterCommandHandler(
        IUserRepository userRepository, 
        IUnitOfWork unitOfWork, 
        IPasswordManager passwordManager, 
        IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordManager = passwordManager;
        _roleRepository = roleRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var email = Email.Create(command.Email);
        if (email.IsError) return email.Errors;

        if ( await _userRepository.GetUserByEmailAsync(email.Value) is not null)  
            return Errors.Authentication.EmailAlreadyExists(email.Value.Value);
        
        var username = Username.Create(command.Username);
        if (username.IsError) return username.Errors;
        
        var firstname = Firstname.Create(command.FirstName);
        if (firstname.IsError) return firstname.Errors;
        
        var lastname = Lastname.Create(command.LastName);
        if (lastname.IsError) return lastname.Errors;
        
        var password = Password.Create(_passwordManager.Secure(command.Password));
        if (password.IsError) return password.Errors;
        
        var profilePicture = ProfilePicture.Create(command.ProfilePictureUrl);
        if (profilePicture.IsError) return profilePicture.Errors;
        
        var role = Role.Create(command.Role);
        if (role.IsError) return role.Errors;
        
        if (await _roleRepository.FindByNameAsync(role.Value, cancellationToken) is null)
            return Errors.Authentication.RoleDoesNotExist(role.Value.Value);
        
        var assignedRoles = new List<Role> {role.Value};



        var user = new User(
            UserId.CreateUnique(), 
            username.Value,
            firstname.Value,
            lastname.Value,
            email.Value,
            password.Value,
            profilePicture.Value,
            assignedRoles
        );

        await _userRepository.AddUserAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var token = "";

        return new AuthenticationResult(user, token);
    }
}