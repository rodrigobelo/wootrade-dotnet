using System;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Dawn;
using Wootrade.Interfaces;
using Wootrade.Model.Enums;
using Wootrade.Model.MarketStream;
using Wootrade.Model.Spot;
using Wootrade.SocketSubClients.Interfaces;

namespace Wootrade.SocketSubClients
{
    internal class WootradeSocketClientSpot : IWootradeSocketClientSpot
    {
        private readonly string applicationId;
        private readonly string baseAddress;
        private readonly WootradeSocketClient baseClient;

        public WootradeSocketClientSpot(WootradeSocketClient baseClient, WootradeSocketClientOptions exchangeOptions)
        {
            this.baseClient = baseClient;
            this.baseAddress = exchangeOptions.BaseAddress;
            this.applicationId = exchangeOptions.ApplicationId;
        }

        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol,
            KlineInterval interval, Action<IWootradeStreamKlineData> onMessage)
        {
            Guard.Argument(symbol).NotNull();

            var handler = new Action<WootradeStreamKlineData>(data => onMessage(data));

            var request = new WootradeStreamSubscriptionRequest("subscribe", $"{symbol}@kline_15m");

            return await Subscribe(request, handler).ConfigureAwait(false);
        }

        private async Task<CallResult<UpdateSubscription>> Subscribe<T>(object request, Action<T> onData)
        {
            var url = baseAddress + "stream/" + this.applicationId;

            return await baseClient.SubscribeInternal(url, request, onData).ConfigureAwait(false);
        }
    }
}