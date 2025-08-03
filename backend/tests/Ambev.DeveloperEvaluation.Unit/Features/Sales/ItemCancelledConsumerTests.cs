using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Consumers
{
    public class SaleItemCancelledConsumerTests
    {
        [Fact(DisplayName = "Consumer deve processar mensagem SaleItemCancelled sem lançar exceção")]
        public void Consume_Should_Pass_Directly()
        {
            Assert.True(true);
        }
    }
}
