using CryptoExchange.Net.Objects;

namespace Wootrade.Model.Spot
{
    public class WootradeClientOptions : RestClientOptions
    {
        public WootradeClientOptions(string baseAddress) : base(baseAddress)
        {
        }

        public WootradeClientOptions() : base("https://api.staging.woo.network/")
        {
        }
    }
}