using System.Linq;
using System.Threading.Tasks;
using Wootrade.Interfaces;
using Wootrade.Model.Spot;
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

            clientOptions.ApiCredentials
                = new CryptoExchange.Net.Authentication.ApiCredentials(apiKey, apiSecret);

            this.client = new WootradeRestClient(clientOptions);
        }

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
    }
}