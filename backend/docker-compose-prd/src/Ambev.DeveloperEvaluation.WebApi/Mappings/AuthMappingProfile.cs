using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;
using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    /// <summary>
    /// AutoMapper profile for authentication mappings.
    /// </summary>
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            // Request → Command
            CreateMap<AuthenticateUserRequest, AuthenticateUserCommand>();
        }
    }
}
