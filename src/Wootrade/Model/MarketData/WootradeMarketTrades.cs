using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wootrade.Model.MarketData
{
    public class WootradeMarketTrades
    {
        [JsonProperty("rows")]
        public IEnumerable<WootradeMarketRecentTrade> RecentTrades { get; set; } = new List<WootradeMarketRecentTrade>();

        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}