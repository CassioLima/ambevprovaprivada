using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Features.Users;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Users
{
    public class DeleteUserRequestValidatorTests
    {
        [Fact(DisplayName = "Validator deve falhar quando Id estiver vazio")]
        public void Validator_Should_Fail_When_Id_Empty()
        {
            var validator = new DeleteUserRequestValidator();
            var result = validator.Validate(new DeleteUserRequest { Id = Guid.Empty });

            Assert.False(result.IsValid);
        }
    }
}
