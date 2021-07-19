using System;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Dawn;
using Wootrade.Interfaces;
using Wootrade.Model;
using Wootrade.Model.Enums;
using Wootrade.Model.MarketStream;
using Wootrade.Model.Spot;
using Wootrade.SocketSubClients.Interfaces;

namespace Wootrade.SocketSubClients
{
    internal class WootradeSocketClientSpot : IWootradeSocketClientSpot
    {
        private readonly string baseAddress;
        private readonly WootradeSocketClient baseClient;

        public WootradeSocketClientSpot(WootradeSocketClient baseClient, WootradeSocketClientOptions exchangeOptions)
        {
            this.baseClient = baseClient;
            this.baseAddress = exchangeOptions.BaseAddress;
        }

        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol,
            KlineInterval interval, Action<IWootradeStreamKlineData> onMessage)
        {
            Guard.Argument(symbol).NotNull();

            string topic = $"{symbol}@kline_{KlineIntervalAdapter.AdaptToString(interval)}";

            var handler = new Action<WootradeStreamKlineData>(
                data =>
                {
                    if (!string.IsNullOrEmpty(data.Topic) && data.Topic.Equals(topic))
                        onMessage(data);
                }
            );

            var request = new WootradeStreamSubscriptionRequest("clientID6", "subscribe", topic);

            return await Subscribe(request, handler).ConfigureAwait(false);
        }

        private async Task<CallResult<UpdateSubscription>> Subscribe<T>(object request, Action<T> onData)
        {
            return await baseClient.SubscribeInternal(baseAddress, request, onData).ConfigureAwait(false);
        }
    }
}