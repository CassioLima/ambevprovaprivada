using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Unit.Application.Auth
{
    public class AuthenticateUserHandlerTests
    {
        [Fact(DisplayName = "Handle deve autenticar usuário válido e retornar token")]
        public async Task Handle_Should_Authenticate_Valid_User_And_Return_Token()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var passwordHasherMock = new Mock<IPasswordHasher>();
            var jwtGeneratorMock = new Mock<IJwtTokenGenerator>();

            // Criando usuário válido
            var fakeUser = new Ambev.DeveloperEvaluation.Domain.Entities.User
            {
                Username = "Test User",
                Email = "test@test.com",
                Phone = "(11) 99999-9999",
                Password = "hashedPassword",
                Role = UserRole.Admin,
                Status = UserStatus.Active
            };

            // Configurando mocks
            userRepositoryMock
                .Setup(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(fakeUser);

            passwordHasherMock
                .Setup(p => p.VerifyPassword(It.IsAny<string>(), fakeUser.Password))
                .Returns(true);

            jwtGeneratorMock
                .Setup(j => j.GenerateToken(fakeUser))
                .Returns("fake-jwt-token");

            var handler = new AuthenticateUserHandler(
                userRepositoryMock.Object,
                passwordHasherMock.Object,
                jwtGeneratorMock.Object
            );

            var command = new AuthenticateUserCommand
            {
                Email = "test@test.com",
                Password = "123456"
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(string.IsNullOrWhiteSpace(result.Token));
            Assert.Equal("fake-jwt-token", result.Token);
        }
    }
}
