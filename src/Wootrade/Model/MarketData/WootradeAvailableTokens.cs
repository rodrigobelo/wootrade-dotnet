using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wootrade.Model.MarketData
{
    public class WootradeAvailableTokens
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("rows")]
        public IEnumerable<WootradeToken> Tokens { get; set; } = new List<WootradeToken>();
    }
}