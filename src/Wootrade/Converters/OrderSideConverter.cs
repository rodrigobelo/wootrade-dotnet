using System;
using System.Collections.Generic;
using System.Linq;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Wootrade.Model.Enums;

namespace Wootrade.Converters
{
    internal class OrderSideConverter : BaseConverter<OrderSide>
    {
        public OrderSideConverter() : this(true)
        {
        }

        public OrderSideConverter(bool quotes) : base(quotes)
        {
        }

        protected override List<KeyValuePair<OrderSide, string>> Mapping => new List<KeyValuePair<OrderSide, string>>
        {
            new KeyValuePair<OrderSide, string>(OrderSide.Buy, "BUY"),
            new KeyValuePair<OrderSide, string>(OrderSide.Sell, "SELL")
        };

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            return Mapping.Single(v => v.Value == (string?)reader.Value).Key;
        }
    }
}