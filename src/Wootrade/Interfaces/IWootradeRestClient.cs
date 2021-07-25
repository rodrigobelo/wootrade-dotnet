using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Wootrade.Model.MarketData;

namespace Wootrade.Interfaces
{
    public interface IWootradeRestClient
    {
        /// <summary>
        /// Get available tokens that WooTrade supported, it need to use when you call get deposit
        /// address or withdraw api.
        /// </summary>
        /// <returns></returns>
        Task<WebCallResult<WootradeAvailableTokens>> GetAvailableTokensAsync();

        /// <summary>
        /// SNAPSHOT of current orderbook. Price of asks/bids are in descending order
        /// </summary>
        /// <param name="symbol">Symbol that you want to query</param>
        /// <returns></returns>
        Task<WebCallResult<WootradeOrderBook>> GetOrderBookAsync(string symbol);

        /// <summary>
        /// Get latest market trades
        /// </summary>
        /// <param name="symbol">Symbol that you want to query</param>
        /// <returns></returns>
        Task<WebCallResult<WootradeMarketTrades>> GetRecentTradesAsync(string symbol);

        /// <summary>
        /// Get data for a specific symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Task<WebCallResult<WootradeSymbolInfo>> GetSymbolAsync(string symbol);

        /// <summary>
        /// Get available symbols that WooTrade supported, and also send order rules for each symbol
        /// </summary>
        /// <returns></returns>
        Task<WebCallResult<WootradeExchangeInfo>> GetSymbolsAsync();
    }
}