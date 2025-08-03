using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

public class CreateUserProfile : Profile
{
    public CreateUserProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();

        CreateMap<CreateUserResult, CreateUserResponse>()
            .ForMember(dest => dest.Name, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.Ignore())
            .ForMember(dest => dest.Phone, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore());
    }
}
