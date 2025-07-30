using Xunit;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;

namespace Ambev.DeveloperEvaluation.Tests.Users
{
    public class DeleteUserProfileTests
    {
        [Fact(DisplayName = "DeleteUserProfile deve ter configuração válida")]
        public void Profile_Should_Have_Valid_Configuration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DeleteUserProfile>();
            });

            config.AssertConfigurationIsValid();
        }
    }
}
