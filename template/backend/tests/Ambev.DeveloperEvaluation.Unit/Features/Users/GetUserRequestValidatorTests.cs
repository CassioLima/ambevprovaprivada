using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Features.Users;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Users
{
    public class GetUserRequestValidatorTests
    {
        [Fact(DisplayName = "Validator deve falhar quando Id estiver vazio")]
        public void Validator_Should_Fail_When_Id_Empty()
        {
            var validator = new GetUserRequestValidator();
            var result = validator.Validate(new GetUserRequest { Id = Guid.Empty });

            Assert.False(result.IsValid);
        }
    }
}
