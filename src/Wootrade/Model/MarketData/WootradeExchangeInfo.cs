using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wootrade.Model.MarketData
{
    public class WootradeExchangeInfo
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("rows")]
        public IEnumerable<WootradeSymbol> Symbols { get; set; } = new List<WootradeSymbol>();
    }
}