using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Wootrade.Model.AccountData
{
    public class WootradeCurrentHoldingEntry
    {
        [JsonProperty("holding")]
        public decimal Holding { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; } = "";

        [JsonProperty("updated_time"), JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime UpdatedTime { get; set; }
    }
}