using Xunit;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;

namespace Ambev.DeveloperEvaluation.Unit.Application.Auth
{
    public class AuthenticateUserValidatorTests
    {
        [Fact(DisplayName = "Validator deve falhar quando usuário estiver vazio")]
        public async Task Validator_Should_Fail_When_Username_Empty()
        {
            // Arrange
            var validator = new AuthenticateUserValidator();
            var command = new AuthenticateUserCommand();

            // Act
            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "Validator deve passar quando dados corretos")]
        public async Task Validator_Should_Pass_When_Data_Correct()
        {
            // Arrange
            var validator = new AuthenticateUserValidator();
            var command = new AuthenticateUserCommand
            {
                Email = "test@test.com",
                Password = "StrongPass1!"
            };

            // Act
            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.True(result.IsValid);
        }

    }
}
