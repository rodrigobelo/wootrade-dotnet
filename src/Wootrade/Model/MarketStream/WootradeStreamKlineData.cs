using Newtonsoft.Json;
using Wootrade.Converters;
using Wootrade.Interfaces;

namespace Wootrade.Model.MarketStream
{
    public class WootradeStreamKlineData : WootradeStreamEvent, IWootradeStreamKlineData
    {
        /// <summary>
        /// The data
        /// </summary>
        [JsonProperty("data")]
        [JsonConverter(typeof(InterfaceConverter<WootradeStreamKline>))]
        public IWootradeStreamKline? Data { get; set; } = default!;

        [JsonProperty("topic")]
        public string Topic { get; set; } = "";
    }
}