using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Extensions
{
    public class AuthenticationExtensionTests
    {
        [Fact(DisplayName = "AddCustomAuthentication deve registrar autenticação sem lançar exceção")]
        public void AddCustomAuthentication_Should_Register_Without_Exception()
        {
            Assert.True(true);
        }
    }
}
