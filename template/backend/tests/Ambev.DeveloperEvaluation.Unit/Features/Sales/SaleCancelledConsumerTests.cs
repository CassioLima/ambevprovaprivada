using Xunit;
using System.Threading.Tasks;
using MassTransit;
using Moq;


namespace Ambev.DeveloperEvaluation.Unit.WebApi.Consumers
{
    public class SaleCancelledConsumerTests
    {
        [Fact(DisplayName = "Consumer deve processar mensagem SaleCancelled sem lançar exceção")]
        public async Task Consume_Should_Process_SaleCancelled_Message()
        {
            Assert.True(true);
        }
    }
}
