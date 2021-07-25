using Newtonsoft.Json;

namespace Wootrade.Model.SpotData
{
    public class WootradeCancelOrderResponse
    {
        /// <summary>
        /// The client_order_id that wish to cancel
        /// </summary>
        [JsonProperty("client_order_id")]
        public int ClientOrderId { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}