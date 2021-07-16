using System;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Dawn;
using Wootrade.Interfaces;
using Wootrade.Model.Enums;
using Wootrade.Model.MarketStream;
using Wootrade.SocketSubClients.Interfaces;

namespace Wootrade.SocketSubClients
{
    internal class WootradeSocketClientSpot : IWootradeSocketClientSpot
    {
        private readonly WootradeSocketClient baseClient;

        public WootradeSocketClientSpot(WootradeSocketClient baseClient, SocketClientOptions exchangeOptions)
        {
            this.baseClient = baseClient;
        }

        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol,
            KlineInterval interval, Action<IWootradeStreamKlineData> onMessage)
        {
            Guard.Argument(symbol).NotNull();

            var handler = new Action<WootradeStreamKlineData>(data => onMessage(data));

            return await Subscribe("kline_1m", handler).ConfigureAwait(false);
        }

        private async Task<CallResult<UpdateSubscription>> Subscribe<T>(string subscriptionTopic, Action<T> onData)
        {
            return await baseClient.SubscribeInternal(subscriptionTopic, onData).ConfigureAwait(false);
        }
    }
}