using System;
using Dawn;
using Newtonsoft.Json;
using Wootrade.Model.Enums;

namespace Wootrade.Model.SpotData
{
    public class WootradeOrderPlacedResponse
    {
        [JsonProperty("order_amount")]
        public decimal? Amount { get; set; }

        [JsonProperty("order_id")]
        public int OrderId { get; set; }

        [JsonProperty("order_price")]
        public decimal? Price { get; set; }

        [JsonProperty("order_quantity")]
        public decimal? Quantity { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("order_type")]
        public OrderType Type { get; set; }
    }

    public class WootradePlaceOrder
    {
        /// <summary>
        /// For MARKET/ASK/BID order, the order size in terms of quote currency
        /// </summary>
        [JsonProperty("order_amount")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// number for scope : from 0 to 9223372036854775807. (default: 0)
        /// </summary>
        [JsonProperty("client_order_id")]
        public int? ClientOrderId { get; set; }

        /// <summary>
        /// If order_type is MARKET, then is not required, otherwise this parameter is required.
        /// </summary>
        [JsonProperty("order_price")]
        public decimal? Price { get; set; }

        /// <summary>
        /// For MARKET/ASK/BID order, if order_amount is given, it is not required.
        /// </summary>
        [JsonProperty("order_quantity")]
        public decimal? Quantity { get; set; }

        /// <summary>
        /// SELL/BUY
        /// </summary>
        [JsonProperty("side")]
        public OrderSide Side { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// An optional tag for this order. (default: default)
        /// </summary>
        [JsonProperty("order_tag")]
        public string? Tag { get; set; }

        /// <summary>
        /// LIMIT/MARKET/IOC/FOK/POST_ONLY
        /// </summary>
        [JsonProperty("order_type")]
        public OrderType Type { get; set; }

        /// <summary>
        /// The order quantity shown on orderbook. (default: equal to order_quantity)
        /// </summary>
        [JsonProperty("visible_quantity")]
        public decimal? VisibleQuantity { get; set; }

        internal void ThrowValidationPlaceOrder()
        {
            Guard.Argument(this.Symbol).NotNull();
            Guard.Argument(this.Side).NotDefault();
            Guard.Argument(this.Type).NotDefault();

            if (this.Quantity != null
                            &&
               !(
                   this.Type == OrderType.Market
                   ||
                   this.Type == OrderType.Ask
                   ||
                   this.Type == OrderType.Bid
                   ||
                   this.Type == OrderType.Limit
               ))
                throw new ArgumentException("Quantity is only valid for Limit, Market, Ask and Bid orders");

            if (this.Amount != null
               &&
               !(
                   this.Type == OrderType.Market
                   ||
                   this.Type == OrderType.Ask
                   ||
                   this.Type == OrderType.Bid
                   ||
                   this.Type == OrderType.Limit
               )
           )
                throw new ArgumentException("Amount is only valid for Limit, Market, Ask and Bid orders");

            if (this.Amount != null && this.Quantity != null)
                throw new ArgumentException("The order would be rejected if both order_amount and order_quantity are provided");

            if (this.Quantity != null)
            {
                var orderValue = this.Quantity * this.Price;

                if (orderValue < 5m)
                {
                    throw new ArgumentException("Order value should be greater or equal to 5.0");
                }

                Guard.Argument(this.Quantity).NotNegative();
            }
        }
    }
}