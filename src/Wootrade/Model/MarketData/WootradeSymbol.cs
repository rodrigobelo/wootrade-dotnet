using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;

namespace Wootrade.Model.MarketData
{
    internal class WootradeSymbol : ICommonSymbol
    {
        decimal ICommonSymbol.CommonMinimumTradeSize => 100;
        string ICommonSymbol.CommonName => Name;
        //LotSizeFilter?.MinQuantity ?? 0;

        /// <summary>
        /// The symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Name { get; set; } = "";
    }
}