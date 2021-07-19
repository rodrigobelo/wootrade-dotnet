using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Wootrade.Model
{
    /// <summary>
    /// A event received by a Wootrade websocket { "event": "ping", "ts": 1626712040001}
    /// </summary>

    public class WootradePingPongStreamEvent : WootradeStreamEvent
    {
        /// <summary>
        /// The event
        /// </summary>
        [JsonProperty("event")]
        public string Event { get; set; } = "";
    }

    public class WootradeStreamEvent
    {
        /// <summary>
        /// The time the event happened
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }

    public class WootradeSubscriptionResponseStreamEvent : WootradeStreamEvent
    {
        /// <summary>
        /// The event
        /// </summary>
        [JsonProperty("event")]
        public string Event { get; set; } = "";

        [JsonProperty("id")]
        public string Id { get; set; } = "";

        [JsonProperty("success")]
        public bool Success { get; set; } = false;
    }
}