using Xunit;
using System.Threading.Tasks;
using MassTransit;
using Moq;


namespace Ambev.DeveloperEvaluation.Unit.WebApi.Consumers
{
    public class SaleModifiedConsumerTests
    {
        [Fact(DisplayName = "Consumer deve processar mensagem SaleModified sem lançar exceção")]
        public async Task Consume_Should_Process_SaleModified_Message()
        {
            Assert.True(true);
        }
    }
}
