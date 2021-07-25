using CryptoExchange.Net.Objects;

namespace Wootrade.Model.Spot
{
    public class WootradeClientOptions : RestClientOptions
    {
        public WootradeClientOptions(string baseAddress) : base(baseAddress)
        {
        }

        /// <summary>
        /// Default base address is staging endpoint address
        /// </summary>
        public WootradeClientOptions() : base("https://api.staging.woo.network/")
        {
        }
    }
}