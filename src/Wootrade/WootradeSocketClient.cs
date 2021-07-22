using System;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Wootrade.Interfaces;
using Wootrade.Model;
using Wootrade.Model.Shared;
using Wootrade.SocketSubClients;
using Wootrade.SocketSubClients.Interfaces;

namespace Wootrade
{
    public class WootradeSocketClient : SocketClient, IWootradeSocketClient
    {
        private const int PongIntervalSeconds = 5;
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
            this.SendPeriodic(TimeSpan.FromSeconds(PongIntervalSeconds), (conn) => new WootradePingPongStreamEvent() { Event = "pong", Timestamp = DateTime.UtcNow });

            return Subscribe(url, request, url, false, onData);
        }

        protected override Task<CallResult<bool>> AuthenticateSocket(SocketConnection s)
        {
            return Task<CallResult<bool>>.FromResult(new CallResult<bool>(false, null));
        }

        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            throw new NotImplementedException();
        }

        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
        {
            var response = JsonConvert.DeserializeObject<WootradeSubscriptionResponseStreamEvent>(message.ToString());

            if (response is object)
            {
                callResult = new CallResult<object>(response, null);
                return true;
            }

            callResult = new CallResult<object>(response, null);

            return false;
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