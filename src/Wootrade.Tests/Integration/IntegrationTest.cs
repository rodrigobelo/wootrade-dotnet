using Wootrade.Interfaces;
using Wootrade.Model.Spot;
using Xunit;

namespace Wootrade.Tests.Integration
{
    public class IntegrationTest
    {
        [Fact]
        public void IntegrationTestMethod()
        {
            WootradeSocketClientOptions clientOptions = new WootradeSocketClientOptions("7fea5769-e03d-469d-8345-ba665270c51a");

            IWootradeSocketClient client = new WootradeSocketClient(clientOptions);
        }
    }
}