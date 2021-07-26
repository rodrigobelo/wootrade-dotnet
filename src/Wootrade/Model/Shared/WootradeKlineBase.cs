using System;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;
using Wootrade.Interfaces;

namespace Wootrade.Model.Shared
{
    /// <summary>
    /// Candlestick information for symbol
    /// </summary>
    public abstract class WootradeKlineBase : IWootradeKline
    {
        /// <summary>
        /// The amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// The price at which this candlestick closed
        /// </summary>
        [JsonProperty("close")]
        public decimal Close { get; set; }

        decimal ICommonKline.CommonClose => Close;

        decimal ICommonKline.CommonHigh => High;

        decimal ICommonKline.CommonLow => Low;

        decimal ICommonKline.CommonOpen => Open;

        DateTime ICommonKline.CommonOpenTime => StartTime;

        decimal ICommonKline.CommonVolume => Volume;

        /// <summary>
        /// The close time of this candlestick
        /// </summary>
        [JsonProperty("endTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// The highest price in this candlestick
        /// </summary>
        [JsonProperty("high")]
        public decimal High { get; set; }

        /// <summary>
        /// The lowest price in this candlestick
        /// </summary>
        [JsonProperty("low")]
        public decimal Low { get; set; }

        /// <summary>
        /// The price at which this candlestick opened
        /// </summary>
        [JsonProperty("open")]
        public decimal Open { get; set; }

        /// The time this candlestick opened </summary>
        [JsonProperty("startTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime StartTime { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// The type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("volume")]
        public decimal Volume { get; set; }
    }
}