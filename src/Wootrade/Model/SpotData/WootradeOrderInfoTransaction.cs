using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Wootrade.Converters;
using Wootrade.Model.Enums;

namespace Wootrade.Model.SpotData
{
    public class WootradeOrderInfoTransaction
    {
        [JsonProperty("executed_price")]
        public decimal ExecutedPrice { get; set; }

        [JsonProperty("executed_quantity")]
        public decimal ExecutedQuantity { get; set; }

        [JsonProperty("executed_timestamp"), JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime ExecutedTimestamp { get; set; }

        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        [JsonProperty("fee_asset")]
        public string FeeAsset { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("is_maker")]
        public bool IsMaker { get; set; }

        [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("order_id")]
        public int WootradeOrderId { get; set; }
    }
}