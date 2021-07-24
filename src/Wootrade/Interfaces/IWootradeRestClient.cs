using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Wootrade.Model.MarketData;

namespace Wootrade.Interfaces
{
    public interface IWootradeRestClient
    {
        /// <summary>
        /// Get a list of available tokens
        /// </summary>
        /// <returns></returns>
        Task<WebCallResult<WootradeAvailableTokens>> GetAvailableTokensAsync();

        /// <summary>
        /// Get list of last trades
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Task<WebCallResult<WootradeMarketTrades>> GetRecentTradesAsync(string symbol);

        /// <summary>
        /// Get data for a specific symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Task<WebCallResult<WootradeSymbolInfo>> GetSymbolAsync(string symbol);

        /// <summary>
        /// Get a list of symbols for the exchange
        /// </summary>
        /// <returns></returns>
        Task<WebCallResult<WootradeExchangeInfo>> GetSymbolsAsync();
    }
}