using Newtonsoft.Json;

namespace Wootrade.Model.MarketData
{
    public class WootradeOrderBookEntry
    {
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }
    }
}