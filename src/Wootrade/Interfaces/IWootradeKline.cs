using System;
using CryptoExchange.Net.ExchangeInterfaces;

namespace Wootrade.Interfaces
{
    /// <summary>
    /// Kline data
    /// </summary>
    public interface IWootradeKline : ICommonKline
    {
        /// <summary>
        /// The amount
        /// </summary>
        decimal Amount { get; set; }

        /// <summary>
        /// The price at which this candlestick closed
        /// </summary>
        decimal Close { get; set; }

        /// <summary>
        /// The close time of this candlestick
        /// </summary>
        DateTime EndTime { get; set; }

        /// <summary>
        /// The highest price in this candlestick
        /// </summary>
        decimal High { get; set; }

        /// <summary>
        /// The lowest price in this candlestick
        /// </summary>
        decimal Low { get; set; }

        /// <summary>
        /// The price at which this candlestick opened
        /// </summary>
        decimal Open { get; set; }

        /// <summary>
        /// The time this candlestick opened
        /// </summary>
        DateTime StartTime { get; set; }

        /// <summary>
        /// KLine Type ({time}: 1m/5m/15m/30m/1h/1d/1w/1M)
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// The volume traded during this candlestick
        /// </summary>
        decimal Volume { get; set; }
    }
}