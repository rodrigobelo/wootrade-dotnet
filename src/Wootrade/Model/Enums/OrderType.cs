namespace Wootrade.Model.Enums
{
    public enum OrderType
    {
        None = 0,

        /// <summary>
        /// it matches until all size executed
        /// </summary>
        Market = 1,

        /// <summary>
        /// it matches as much as possible at the order_price. If not fully executed, then remaining
        /// quantity will be cancelled.
        /// </summary>
        IOC = 2,

        /// <summary>
        /// if the order can be fully executed at the order_price then the order gets fully executed
        /// otherwise would be cancelled without any execution
        /// </summary>
        FOK = 3,

        /// <summary>
        /// the order price is guranteed to be the best ask price of the orderbook if it gets accepted
        /// </summary>
        Ask = 4,

        /// <summary>
        /// the order price is guranteed to be the best bid price of the orderbook if it gets accepted
        /// </summary>
        Bid = 5,

        /// <summary>
        /// the order price is set and guranteed
        /// </summary>
        Limit = 6
    }
}