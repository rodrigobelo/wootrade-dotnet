using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wootrade.Model.SpotData
{
    internal class WootradeGetOrdersPage
    {
        [JsonProperty("meta")]
        public OrdersMetadata Meta { get; set; } = new OrdersMetadata();

        [JsonProperty("rows")]
        public IEnumerable<WootradeOrderInfo> Orders { get; set; } = new List<WootradeOrderInfo>();

        [JsonProperty("success")]
        public bool Success { get; set; }

        public class OrdersMetadata
        {
            [JsonProperty("current_page")]
            public int CurrentPage { get; set; }

            [JsonProperty("records_per_page")]
            public int RecordsPerPage { get; set; }
        }
    }
}