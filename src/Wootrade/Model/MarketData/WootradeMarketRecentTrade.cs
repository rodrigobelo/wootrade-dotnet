using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Wootrade.Model.MarketData
{
    public class WootradeMarketRecentTrade
    {
        [JsonProperty("executed_price")]
        public decimal ExecutedPrice { get; set; }

        [JsonProperty("executed_quantity")]
        public decimal ExecutedQuantity { get; set; }

        [JsonProperty("executed_timestamp"), JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime ExecutedTimestamp { get; set; }

        [JsonProperty("side")]
        public string Side { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}