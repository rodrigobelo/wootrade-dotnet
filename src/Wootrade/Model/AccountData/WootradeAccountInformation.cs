using Newtonsoft.Json;

namespace Wootrade.Model.AccountData
{
    public class WootradeAccountInformation
    {
        [JsonProperty("application")]
        public WootradeApplication Application { get; set; }

        [JsonProperty("margin_rate")]
        public decimal MarginRate { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}