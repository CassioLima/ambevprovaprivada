using AutoMapper;
using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth;
using Ambev.DeveloperEvaluation.WebApi.Mappings;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Auth
{
    public class AuthMappingProfileTests
    {
        [Fact(DisplayName = "AuthMappingProfile deve ter configuração válida")]
        public void Profile_Should_Have_Valid_Configuration()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AuthMappingProfile>());
            config.AssertConfigurationIsValid();
        }
    }
}
