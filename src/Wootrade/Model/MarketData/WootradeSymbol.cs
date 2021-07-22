using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;

namespace Wootrade.Model.MarketData
{
    public class WootradeSymbol : ICommonSymbol
    {
        /* "created_time": "1575441595.65",
      "updated_time": "1575441595.65",
      "symbol": "SPOT_BTC_USDT",
      "quote_min": 100,
      "quote_max": 100000,
      "quote_tick": 0.01,
      "base_min": 0.0001,
      "base_tick": 0.0001,
      "min_notional": 0.02,
      "price_range": 0.99
         *
         * */

        public decimal CommonMinimumTradeSize => MinimumQuote;

        public string CommonName => Name;

        [JsonProperty("quote_min")]
        public decimal MinimumQuote { get; set; }

        [JsonProperty("symbol")]
        public string Name { get; set; } = "";
    }
}