using System.Linq;
using System.Threading.Tasks;
using Wootrade.Interfaces;
using Wootrade.Model.Spot;
using Xunit;

namespace Wootrade.Tests.Integrated
{
    public class WootradePublicMethodsSuite
    {
        private const string WootradeStagingEndpoint = "https://api.staging.woo.network/";
        private IWootradeRestClient client;

        public WootradePublicMethodsSuite()
        {
            WootradeClientOptions clientOptions = new WootradeClientOptions(WootradeStagingEndpoint);
            this.client = new WootradeRestClient(clientOptions);
        }

        [Fact]
        public async Task WootradeRestClient_GetAvailableTokensAsync_Success()
        {
            var result = await client.GetAvailableTokensAsync();

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Success);
            Assert.True(result.Data.Tokens.Any());
            Assert.True(result.Data.Tokens.First(t => t.Token.Equals("BTC")).Decimals.Equals(8));
        }

        [Fact]
        public async Task WootradeRestClient_GetRecentTradesAsync_Success()
        {
            var result = await client.GetRecentTradesAsync("SPOT_ETH_USDT");

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.RecentTrades.Any());
            Assert.True(result.Data.RecentTrades.All(t => t.Symbol.Equals("SPOT_ETH_USDT")));
        }

        [Fact]
        public async Task WootradeRestClient_GetSymbolAsync_Success()
        {
            var result = await client.GetSymbolAsync("SPOT_ETH_USDT");

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Success);
            Assert.True(result.Data.Symbol.Name.Equals("SPOT_ETH_USDT"));
        }

        [Fact]
        public async Task WootradeRestClient_GetSymbolsAsync_Success()
        {
            var result = await client.GetSymbolsAsync();

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Success);
            Assert.True(result.Data.Symbols.Any());
        }
    }
}