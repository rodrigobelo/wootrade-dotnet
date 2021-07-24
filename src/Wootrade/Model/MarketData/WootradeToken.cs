using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Wootrade.Model.MarketData
{
    public class WootradeToken
    {
        [JsonProperty("balance_token")]
        public string BalanceToken { get; set; }

        [JsonProperty("created_time"), JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("decimals")]
        public int Decimals { get; set; }

        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("updated_time"), JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime UpdatedTime { get; set; }
    }
}