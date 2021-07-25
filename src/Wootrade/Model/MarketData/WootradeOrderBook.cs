using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Wootrade.Model.MarketData
{
    public class WootradeOrderBook
    {
        /// <summary>
        /// Asks
        /// </summary>
        [JsonProperty("asks")]
        public IEnumerable<WootradeOrderBookEntry> Asks { get; set; } = new List<WootradeOrderBookEntry>();

        /// <summary>
        /// Bids
        /// </summary>
        [JsonProperty("bids")]
        public IEnumerable<WootradeOrderBookEntry> Bids { get; set; } = new List<WootradeOrderBookEntry>();

        [JsonProperty("success")]
        public bool Success { get; set; }

        public string Symbol { get; set; }

        [JsonProperty("timestamp"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }
}