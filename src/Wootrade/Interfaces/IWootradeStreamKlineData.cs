namespace Wootrade.Interfaces
{
    public interface IWootradeStreamKlineData
    {
        /// <summary>
        /// The data
        /// </summary>
        IWootradeStreamKline Data { get; set; }

        /// <summary>
        /// The topic the data is for
        /// </summary>
        string Topic { get; set; }
    }
}