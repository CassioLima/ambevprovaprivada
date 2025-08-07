using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Features.Users;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Users
{
    public class CreateUserRequestValidatorTests
    {
        [Fact(DisplayName = "Validator deve falhar quando email estiver vazio")]
        public void Validator_Should_Fail_When_Email_Empty()
        {
            var validator = new CreateUserRequestValidator();
            var result = validator.Validate(new CreateUserRequest { Email = "" });

            Assert.False(result.IsValid);
        }
    }
}
