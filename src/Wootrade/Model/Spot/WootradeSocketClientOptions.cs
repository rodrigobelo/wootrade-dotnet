using CryptoExchange.Net.Objects;

namespace Wootrade.Model.Spot
{
    public class WootradeSocketClientOptions : SocketClientOptions
    {
        public WootradeSocketClientOptions(string baseAddress, string applicationId) : base(baseAddress)
        {
            ApplicationId = applicationId;
        }

        public WootradeSocketClientOptions(string applicationId) : base("wss://wss.staging.woo.network/ws/stream/{application_id}")
        {
            ApplicationId = applicationId;
        }

        public string ApplicationId { get; set; }
    }
}