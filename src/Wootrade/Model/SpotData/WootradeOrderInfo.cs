using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Wootrade.Converters;
using Wootrade.Model.Enums;

namespace Wootrade.Model.SpotData
{
    public class WootradeOrderInfo
    {
        [JsonProperty("amount")]
        public decimal? Amount { get; set; }

        [JsonProperty("client_order_id")]
        public int? ClientOrderId { get; set; }

        [JsonProperty("created_time"), JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("executed")]
        public bool Executed { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }


        [JsonProperty("average_executed_price")]
        public decimal? AverageExecutedPrice { get; set; }

        [JsonProperty("quantity")]
        public decimal? Quantity { get; set; }

        [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }

        [JsonProperty("status"), JsonConverter(typeof(OrderStatusConverter))]
        public OrderStatus Status { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("order_tag")]
        public string Tag { get; set; }

        [JsonProperty("transactions")]
        public IEnumerable<WootradeOrderInfoTransaction> Transactions { get; set; } = new List<WootradeOrderInfoTransaction>();

        [JsonProperty("type"), JsonConverter(typeof(OrderTypeConverter))]
        public OrderType Type { get; set; }

        [JsonProperty("visible")]
        public decimal Visible { get; set; }

        [JsonProperty("order_id")]
        public int WootradeOrderId { get; set; }
    }
}