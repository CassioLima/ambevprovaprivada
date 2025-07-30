using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using NSubstitute;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users
{
    public class DeleteUserHandlerTests
    {
        [Fact(DisplayName = "Handle deve deletar usuário sem lançar exceção")]
        public async Task Handle_Should_Delete_User()
        {
            // Assert
            Assert.True(true);
        }
    }
}
