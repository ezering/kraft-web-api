using Application.CQRS.Authentication.Commands.Register;
using Application.CQRS.Authentication.Common;
using Application.CQRS.Authentication.Queries.Login;
using Mapster;

namespace Presentation.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Id, src => src.User.Id.Value)
            .Map(dest => dest.Username, src => src.User.Username.Value)
            .Map(dest => dest.FirstName, src => src.User.Firstname.Value)
            .Map(dest => dest.LastName, src => src.User.Lastname.Value)
            .Map(dest => dest.Email, src => src.User.Email.Value)
            .Map(dest => dest.ProfilePictureUrl, src => src.User.ProfilePicture.Value)
            .Map(dest => dest.AccessToken, src => src.AccessToken);
    }
}