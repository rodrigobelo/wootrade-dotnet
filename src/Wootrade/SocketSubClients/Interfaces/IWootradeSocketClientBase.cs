using System;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Wootrade.Interfaces;
using Wootrade.Model.Enums;

namespace Wootrade.SocketSubClients.Interfaces
{
    public interface IWootradeSocketClientBase
    {
        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>
        /// A stream subscription. This stream subscription can be used to be notified when the
        /// socket is disconnected/reconnected
        /// </returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<IWootradeStreamKlineData> onMessage);
    }
}