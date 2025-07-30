using Xunit;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

namespace Ambev.DeveloperEvaluation.Tests.Users
{
    public class GetUserProfileTests
    {
        [Fact(DisplayName = "GetUserProfile deve ter configuração válida")]
        public void Profile_Should_Have_Valid_Configuration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GetUserProfile>();
            });

            config.AssertConfigurationIsValid();
        }
    }
}
