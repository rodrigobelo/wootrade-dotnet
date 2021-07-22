using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;

namespace Wootrade.Interfaces
{
    public interface IWootradeRestClient
    {
        /// <summary>
        /// Get a list of symbols for the exchange
        /// </summary>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<ICommonSymbol>>> GetSymbolsAsync();
    }
}