using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Wootrade.Model
{
    /// <summary>
    /// A event received by a Wootrade websocket
    /// </summary>

    public class WootradeStreamEvent
    {
        /// <summary>
        /// The time the event happened
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The symbol the data is for
        /// </summary>
        [JsonProperty("topic")]
        public string Topic { get; set; } = "";
    }
}