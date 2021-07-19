using Newtonsoft.Json;

namespace Wootrade.Model.MarketStream
{
    public class WootradeStreamSubscriptionRequest
    {
        public WootradeStreamSubscriptionRequest(string id, string eventStr, string subscriptionTopic)
        {
            this.Id = id;
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