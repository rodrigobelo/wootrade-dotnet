namespace Wootrade.Interfaces
{
    /// <summary>
    /// Stream kline data
    /// </summary>
    public interface IWootradeStreamKline : IWootradeKline
    {
        /// <summary>
        /// The symbol the data is for
        /// </summary>
        string Symbol { get; set; }
    }
}