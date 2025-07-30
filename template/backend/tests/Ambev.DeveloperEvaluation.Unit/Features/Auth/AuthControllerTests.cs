using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Auth
{
    public class AuthControllerTests
    {
        private readonly AuthController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;

        public AuthControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _controller = new AuthController(_mediatorMock.Object, _mapperMock.Object);
        }

        [Fact(DisplayName = "AuthenticateUser deve retornar Ok com token quando credenciais válidas")]
        public async Task AuthenticateUser_Should_Return_Ok_When_Valid()
        {
            // Arrange
            var request = new AuthenticateUserRequest { Email = "teste@teste.com", Password = "Senha@123" };
            var commandResult = new AuthenticateUserResult
            {
                Email = "teste@teste.com",
                Name = "Usuário Teste",
                Role = "Admin",
                Token = "fake-jwt-token"
            };
            var response = new AuthenticateUserResponse { Token = commandResult.Token };

            _mapperMock.Setup(m => m.Map<AuthenticateUserCommand>(request))
                       .Returns(new AuthenticateUserCommand { Email = request.Email, Password = request.Password });

            _mediatorMock.Setup(m => m.Send(It.IsAny<AuthenticateUserCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(commandResult);

            _mapperMock.Setup(m => m.Map<AuthenticateUserResponse>(commandResult))
                       .Returns(response);

            // Act
            var result = await _controller.AuthenticateUser(request, default);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponseWithData<ApiResponseWithData<AuthenticateUserResponse>>>(okResult.Value);

            Assert.True(apiResponse.Success);
            Assert.Equal("fake-jwt-token", apiResponse.Data.Data.Token);
        }
    }
}
