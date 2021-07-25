using System.Linq;
using System.Threading.Tasks;
using Wootrade.Interfaces;
using Wootrade.Model.Enums;
using Wootrade.Model.Spot;
using Wootrade.Model.SpotData;
using Xunit;

namespace Wootrade.Tests.Integrated
{
    public class WootradeAuthenticatedMethodsSuite
    {
        private const string WootradeProductionEndpoint = "https://api.woo.network/";
        private IWootradeRestClient client;

        public WootradeAuthenticatedMethodsSuite()
        {
            WootradeClientOptions clientOptions = new WootradeClientOptions(WootradeProductionEndpoint);
            clientOptions.LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug;

            var apiKey = System.Environment.GetEnvironmentVariable("WOO_TESTS_API_KEY");
            var apiSecret = System.Environment.GetEnvironmentVariable("WOO_TESTS_API_SECRET");

            this.client = new WootradeRestClient(clientOptions);
        }

        [Fact(Skip = "Skipped")]
        public async Task WootradeRestClient_CancelOrderAsync_Success()
        {
            var symbol = "SPOT_WOO_USDT";
            var result = await client.CancelOrderAsync(symbol, 1);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(symbol, result.Data.Symbol);
        }

        [Fact]
        public async Task WootradeRestClient_GetCurrentHoldingsAsync_Success()
        {
            var result = await client.GetCurrentHoldingAsync(true);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Success);
            Assert.NotNull(result.Data.Holding);
            Assert.True(result.Data.Holding.Any());
        }

        [Fact(Skip = "Skipped")]
        public async Task WootradeRestClient_GetOrderAsync_Success()
        {
            var result = await client.GetOrderAsync(1);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Success);
            Assert.NotNull(result.Data.Symbol);
        }

        [Fact]
        public async Task WootradeRestClient_GetOrderBookAsync_Success()
        {
            var result = await client.GetOrderBookAsync("SPOT_ETH_USDT");

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Success);
            Assert.NotNull(result.Data.Symbol);
            Assert.True(result.Data.Asks.Any());
            Assert.True(result.Data.Bids.Any());
        }

        [Fact(Skip = "Skipped")]
        public async Task WootradeRestClient_PlaceOrderAsync_Success()
        {
            WootradePlaceOrder order = new WootradePlaceOrder();

            order.ClientOrderId = 1;
            order.Price = 0.1m;
            order.Quantity = 100m;
            order.Side = OrderSide.Sell;
            order.Symbol = "SPOT_WOO_USDT";
            order.Tag = "Test";
            order.Type = OrderType.Limit;

            var result = await client.PlaceOrderAsync(order);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Success);

            Assert.True(result.Data.Price == 0.1m);
            Assert.NotNull(result.Data.Quantity);
            Assert.True(result.Data.Quantity.Value == 100m);
        }
    }
}