using System;
using Wootrade.Model.Enums;

namespace Wootrade.Model.SpotData
{
    public class GetOrdersFilter
    {
        public DateTime? EndDate { get; set; }

        public OrderSide? Side { get; set; }

        public DateTime? StartDate { get; set; }

        public OrderStatus? Status { get; set; }

        public string? Symbol { get; set; }

        public string? Tag { get; set; }

        public OrderType? Type { get; set; }

        internal int Page { get; set; }
    }
}