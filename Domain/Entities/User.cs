using Domain.Primitives;
using Domain.ValueObjects.Users;

namespace Domain.Entities;

public sealed class User:BaseEntity<UserId>
{
    public UserId Id { get; private set; }
    public Username Username { get; private set; } 
    public Firstname Firstname { get; private set; }
    public Lastname Lastname { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public List<Role> Roles { get; private set; }
    public ProfilePicture ProfilePicture { get; private set; }
    
    public User(
        UserId id, 
        Username username,
        Firstname firstname, 
        Lastname lastname, 
        Email email, 
        Password password, 
        ProfilePicture profilePicture, 
        List<Role> roles) 
        : base(id, DateTime.UtcNow, DateTime.UtcNow, null)
    {
        Id = id;
        Username = username;
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        Password = password;
        ProfilePicture = profilePicture;
        Roles = roles;
    }
}