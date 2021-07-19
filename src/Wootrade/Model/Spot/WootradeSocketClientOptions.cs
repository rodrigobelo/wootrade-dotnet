using CryptoExchange.Net.Objects;

namespace Wootrade.Model.Spot
{
    public class WootradeSocketClientOptions : SocketClientOptions
    {
        public WootradeSocketClientOptions(string applicationId, string baseAddress) : base(baseAddress + applicationId)
        {
            ApplicationId = applicationId;
        }

        public WootradeSocketClientOptions(string applicationId) : base("wss://wss.woo.network/ws/stream/" + applicationId)
        {
            ApplicationId = applicationId;
        }

        public string ApplicationId { get; set; }
    }
}