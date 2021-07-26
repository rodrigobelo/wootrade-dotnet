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
        private const int TestOrderId = 33618629;
        private const string WootradeProductionEndpoint = "https://api.woo.network/";
        private IWootradeRestClient client;

        public WootradeAuthenticatedMethodsSuite()
        {
            WootradeClientOptions clientOptions = new WootradeClientOptions(WootradeProductionEndpoint);
            clientOptions.LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug;

            var apiKey = System.Environment.GetEnvironmentVariable("WOO_TESTS_API_KEY");
            var apiSecret = System.Environment.GetEnvironmentVariable("WOO_TESTS_API_SECRET");

            clientOptions.ApiCredentials
                = new CryptoExchange.Net.Authentication.ApiCredentials(apiKey, apiSecret);

            this.client = new WootradeRestClient(clientOptions);
        }

        [Fact(Skip = "Skipped")]
        public async Task WootradeRestClient_CancelOrderByClientIdAsync_Success()
        {
            var symbol = "SPOT_WOO_USDT";
            var result = await client.CancelOrderByClientIdAsync(symbol, 1);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("CANCEL_SENT", result.Data.Status);
        }

        [Fact(Skip = "Skipped")]
        public async Task WootradeRestClient_CancelOrderByWootradeOrderIdAsync_Success()
        {
            var symbol = "SPOT_WOO_USDT";
            var result = await client.CancelOrderByWootradeOrderIdAsync(symbol, 33618629);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("CANCEL_SENT", result.Data.Status);
        }

        [Fact]
        public async Task WootradeRestClient_GetCurrentHoldingsAsync_Success()
        {
            var result = await client.GetCurrentHoldingAsync(false);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Success);
            Assert.NotNull(result.Data.Holding);
            Assert.True(result.Data.Holding.Any());
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

        [Fact]
        public async Task WootradeRestClient_GetOrderByClientOrderIdAsync_Success()
        {
            var result = await client.GetOrderByClientOrderIdAsync(1);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Success);
            Assert.NotNull(result.Data.Symbol);
            Assert.Equal("SPOT_WOO_USDT", result.Data.Symbol);
            Assert.Equal(15, result.Data.Quantity);
            Assert.Equal(0.4m, result.Data.Price);
            Assert.Equal(OrderSide.Buy, result.Data.Side);
            Assert.Equal("Test", result.Data.Tag);
        }

        [Fact(Skip = "Skipped")]
        public async Task WootradeRestClient_GetOrderByWootradeOrderIdAsync_Success()
        {
            var result = await client.GetOrderByWootradeOrderIdAsync(TestOrderId);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Success);
            Assert.NotNull(result.Data.Symbol);
            Assert.Equal("SPOT_WOO_USDT", result.Data.Symbol);
            Assert.Equal(15, result.Data.Quantity);
            Assert.Equal(0.4m, result.Data.Price);
            Assert.Equal(OrderSide.Buy, result.Data.Side);
            Assert.Equal("Test", result.Data.Tag);
        }

        [Fact(Skip = "Skipped")]
        public async Task WootradeRestClient_PlaceOrderAsync_Success()
        {
            WootradePlaceOrder order = new WootradePlaceOrder();

            order.ClientOrderId = 1;
            order.Price = 0.4m;
            order.Quantity = 15;
            order.Side = OrderSide.Buy;
            order.Symbol = "SPOT_WOO_USDT";
            order.Tag = "Test";
            order.Type = OrderType.Limit;

            var result = await client.PlaceOrderAsync(order);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Success);

            Assert.True(result.Data.Price == 0.4m);
            Assert.NotNull(result.Data.Quantity);
            Assert.True(result.Data.Quantity.Value == 15m);
        }
    }
}