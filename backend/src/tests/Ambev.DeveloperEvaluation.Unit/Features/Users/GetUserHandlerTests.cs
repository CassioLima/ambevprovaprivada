using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users
{
    public class GetUserHandlerTests
    {
        [Fact(DisplayName = "Handle deve buscar usuário sem lançar exceção")]
        public async Task Handle_Should_Get_User()
        {
            // Arrange
            var repositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var handler = new GetUserHandler(repositoryMock.Object, mapperMock.Object);

            var command = new GetUserCommand(Guid.NewGuid());

            var fakeUser = new Ambev.DeveloperEvaluation.Domain.Entities.User
            {
                Username = "Teste",
                Email = "teste@teste.com",
                Phone = "123456789",
                Password = "SenhaSegura@123",
                Role = UserRole.Customer,  
                Status = UserStatus.Active
            };

            var fakeResult = new GetUserResult
            {
                Id = command.Id,
                Name = fakeUser.Username,
                Email = fakeUser.Email,
                Phone = fakeUser.Phone,
                Role = fakeUser.Role,
                Status = fakeUser.Status
            };

            repositoryMock.Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(fakeUser);

            mapperMock.Setup(m => m.Map<GetUserResult>(fakeUser)).Returns(fakeResult);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.Id, result.Id);
            Assert.Equal("teste@teste.com", result.Email);
        }
    }
}
