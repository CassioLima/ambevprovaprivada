using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.WebApi.Features.Users;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Microsoft.AspNetCore.Http;

namespace Ambev.DeveloperEvaluation.Tests.WebApi.Controllers
{
    public class UsersControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _controller = new UsersController(_mediatorMock.Object, _mapperMock.Object);
        }

        [Fact(DisplayName = "CreateUser deve retornar Created quando dados válidos")]
        public async Task CreateUser_Should_Return_Created_When_Valid()
        {
            // Arrange
            var request = new CreateUserRequest
            {
                Username = "usuario1",
                Email = "teste@teste.com",
                Password = "Senha@123",
                Phone = "+5511999999999",
                Role = UserRole.Admin,
                Status = UserStatus.Active
            };

            var command = new CreateUserCommand
            {
                Username = request.Username,
                Password = request.Password,
                Email = request.Email,
                Phone = request.Phone,
                Role = request.Role,
                Status = request.Status
            };

            var result = new CreateUserResult { Id = Guid.NewGuid() };
            var response = new CreateUserResponse
            {
                Id = result.Id,
                Name = request.Username,
                Email = request.Email,
                Phone = request.Phone,
                Role = request.Role,
                Status = request.Status
            };

            _mapperMock.Setup(m => m.Map<CreateUserCommand>(request)).Returns(command);
            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(result);
            _mapperMock.Setup(m => m.Map<CreateUserResponse>(result)).Returns(response);

            // Act
            var actionResult = await _controller.CreateUser(request, CancellationToken.None);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(actionResult);
            var apiResponse = Assert.IsType<ApiResponseWithData<CreateUserResponse>>(createdResult.Value);
            Assert.True(apiResponse.Success);
            Assert.Equal(response.Email, apiResponse.Data.Email);
        }

        [Fact(DisplayName = "CreateUser deve retornar Created quando dados inválidos (sem validação na controller)")]
        public async Task CreateUser_Should_Return_Created_When_Invalid()
        {
            // Arrange
            var request = new CreateUserRequest(); // sem dados obrigatórios

            // Act
            var actionResult = await _controller.CreateUser(request, default);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(actionResult);
            var apiResponse = Assert.IsType<ApiResponseWithData<CreateUserResponse>>(createdResult.Value);

            Assert.True(apiResponse.Success);
            Assert.Equal("User created successfully", apiResponse.Message);
        }


        [Fact(DisplayName = "GetUser deve retornar Ok com dados do usuário")]
        public async Task GetUser_Should_Return_Ok_With_User_Data()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var command = new GetUserCommand(userId);
            var result = new GetUserResult
            {
                Id = userId,
                Name = "Teste",
                Email = "teste@teste.com",
                Phone = "12345678"
            };
            var response = new GetUserResponse
            {
                Id = userId,
                Name = result.Name,
                Email = result.Email,
                Phone = result.Phone
            };

            // Mock correto do mapper (controller usa GetUserRequest → GetUserCommand)
            _mapperMock.Setup(m => m.Map<GetUserCommand>(It.IsAny<GetUserRequest>()))
                       .Returns(command);

            // Mock do mediator
            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(result);

            // Mock do mapper para resposta
            _mapperMock.Setup(m => m.Map<GetUserResponse>(result))
                       .Returns(response);

            // Act
            var actionResult = await _controller.GetUser(userId, default);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);

            Assert.True(okResult.StatusCode == 200);

        }

        [Fact(DisplayName = "DeleteUser deve retornar Ok com sucesso")]
        public async Task DeleteUser_Should_Return_Ok_With_Success()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var command = new DeleteUserCommand(userId);

            _mapperMock.Setup(m => m.Map<DeleteUserCommand>(userId)).Returns(command);
            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(new DeleteUserResponse());

            // Act
            var actionResult = await _controller.DeleteUser(userId, default);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.True(okResult.StatusCode == 200);
        }

    }
}
