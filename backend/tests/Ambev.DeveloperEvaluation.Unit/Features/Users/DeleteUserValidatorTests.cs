using Xunit;
using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users
{
    public class DeleteUserValidatorTests
    {
        [Fact(DisplayName = "Validator deve falhar quando Id for vazio")]
        public void Validator_Should_Fail_When_Id_Empty()
        {
            var validator = new DeleteUserValidator();
            var result = validator.Validate(new DeleteUserCommand(Guid.Empty));

            Assert.False(result.IsValid);
        }
    }
}
