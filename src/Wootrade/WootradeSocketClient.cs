using System;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json.Linq;
using Wootrade.Interfaces;
using Wootrade.Model.Spot;
using Wootrade.SocketSubClients;
using Wootrade.SocketSubClients.Interfaces;

namespace Wootrade
{
    public class WootradeSocketClient : SocketClient, IWootradeSocketClient
    {
        private static WootradeSocketClientOptions defaultOptions = new WootradeSocketClientOptions(string.Empty);

        public WootradeSocketClient() : this(defaultOptions)
        { }

        public WootradeSocketClient(WootradeSocketClientOptions exchangeOptions) : base("Wootrade", exchangeOptions, exchangeOptions.ApiCredentials == null ? null : new WootradeAuthenticationProvider(exchangeOptions.ApiCredentials))
        {
            this.Spot = new WootradeSocketClientSpot(this, exchangeOptions);
        }

        /// <summary>
        /// Spot streams
        /// </summary>
        public IWootradeSocketClientSpot Spot { get; set; }

        internal Task<CallResult<UpdateSubscription>> SubscribeInternal<T>(string url, object request, Action<T> onData)
        {
            return Subscribe(url, request, url + NextId(), false, onData);
        }

        protected override Task<CallResult<bool>> AuthenticateSocket(SocketConnection s)
        {
            throw new NotImplementedException();
        }

        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            throw new NotImplementedException();
        }

        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
        {
            throw new NotImplementedException();
        }

        protected override bool MessageMatchesHandler(JToken message, object request)
        {
            return true;
        }

        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            return true;
        }

        protected override Task<bool> Unsubscribe(SocketConnection connection, SocketSubscription s)
        {
            return Task.FromResult(true);
        }
    }
}