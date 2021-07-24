using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;

namespace Wootrade.Model.MarketData
{
    public class WootradeSymbol : ICommonSymbol
    {
        public decimal CommonMinimumTradeSize => MinimumQuote;

        public string CommonName => Name;

        [JsonProperty("quote_min")]
        public decimal MinimumQuote { get; set; }

        [JsonProperty("symbol")]
        public string Name { get; set; } = "";

        [JsonProperty("price_range")]
        public decimal PriceRange { get; set; }
    }
}