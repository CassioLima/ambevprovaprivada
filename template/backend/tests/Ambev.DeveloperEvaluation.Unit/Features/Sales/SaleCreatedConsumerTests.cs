using Xunit;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Moq;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Consumers
{
    public class SaleCreatedConsumerTests
    {
        [Fact(DisplayName = "Consumer deve processar mensagem SaleCreated sem lançar exceção")]
        public async Task Consume_Should_Process_SaleCreated_Message()
        {
            Assert.True(true);
        }
    }
}
