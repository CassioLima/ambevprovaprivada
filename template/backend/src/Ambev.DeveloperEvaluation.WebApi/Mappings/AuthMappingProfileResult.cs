using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class AuthMappingProfileResult : Profile
{
    public AuthMappingProfileResult()
    {
        CreateMap<AuthenticateUserResult, AuthenticateUserResponse>()
            .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
    }
}