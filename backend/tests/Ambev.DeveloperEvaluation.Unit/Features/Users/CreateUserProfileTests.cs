using Xunit;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

namespace Ambev.DeveloperEvaluation.Tests.Users
{
    public class CreateUserProfileTests
    {
        [Fact(DisplayName = "CreateUserProfile deve ter configuração válida")]
        public void Profile_Should_Have_Valid_Configuration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CreateUserProfile>();
            });

            config.AssertConfigurationIsValid();
        }
    }
}
