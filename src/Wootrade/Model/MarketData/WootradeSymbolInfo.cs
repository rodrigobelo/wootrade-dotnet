using Newtonsoft.Json;

namespace Wootrade.Model.MarketData
{
    public class WootradeSymbolInfo
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("info")]
        public WootradeSymbol Symbol { get; set; }
    }
}