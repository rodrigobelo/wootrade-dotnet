using Newtonsoft.Json;

namespace Wootrade.Model.MarketStream
{
    /*
     * {
"id": "clientID6",
"topic": "SPOT_BTC_USDT@kline_1m",
"event": "subscribe"
}
     * */

    public class WootradeStreamSubscriptionRequest
    {
        public WootradeStreamSubscriptionRequest(string eventStr, string subscriptionTopic)
        {
            this.Id = string.Empty;
            this.Event = eventStr;
            this.SubscriptionTopic = subscriptionTopic;
        }

        [JsonProperty("event")]
        public string Event { get; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("topic")]
        public string SubscriptionTopic { get; }
    }
}