using CryptoExchange.Net.Objects;

namespace Wootrade.Model.Spot
{
    public class WootradeSocketClientOptions : SocketClientOptions
    {
        public WootradeSocketClientOptions(string baseAddress) : base(baseAddress)
        {
        }

        public WootradeSocketClientOptions() : base("wss://wss.staging.woo.network/ws/stream/{application_id}")
        {
        }
    }
}