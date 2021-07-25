using System;
using System.Collections.Generic;
using System.Linq;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Wootrade.Model.Enums;

namespace Wootrade.Converters
{
    internal class OrderStatusConverter : BaseConverter<OrderStatus>
    {
        public OrderStatusConverter() : this(true)
        {
        }

        public OrderStatusConverter(bool quotes) : base(quotes)
        {
        }

        protected override List<KeyValuePair<OrderStatus, string>> Mapping => new List<KeyValuePair<OrderStatus, string>>
        {
            new KeyValuePair<OrderStatus, string>(OrderStatus.New, "NEW"),
            new KeyValuePair<OrderStatus, string>(OrderStatus.PartialFilled, "PARTIAL_FILLED"),
            new KeyValuePair<OrderStatus, string>(OrderStatus.Rejected, "REJECTED"),
            new KeyValuePair<OrderStatus, string>(OrderStatus.Filled, "FILLED"),
            new KeyValuePair<OrderStatus, string>(OrderStatus.Incomplete, "INCOMPLETE"),
            new KeyValuePair<OrderStatus, string>(OrderStatus.Complete, "COMPLETED")
        };

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            return Mapping.Single(v => v.Value == (string?)reader.Value).Key;
        }
    }
}