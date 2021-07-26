using Newtonsoft.Json;

namespace Wootrade.Model.SpotData
{
    public class WootradeCancelOrderResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; } = "";

        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}