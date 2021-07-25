using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Wootrade.Interfaces;
using Wootrade.Model.MarketData;
using Wootrade.Model.Spot;

namespace Wootrade
{
    public class WootradeRestClient : RestClient, IWootradeRestClient
    {
        private const string apiVersion = "v1";
        private const string publicPath = "public";

        public WootradeRestClient() : this(new WootradeClientOptions())
        { }

        public WootradeRestClient(WootradeClientOptions clientOptions)
            : base("Wootrade", clientOptions, clientOptions.ApiCredentials == null ? null : new WootradeAuthenticationProvider(clientOptions.ApiCredentials))
        {
        }

        public async Task<WebCallResult<WootradeAvailableTokens>> GetAvailableTokensAsync()
        {
            var exchangeInfoResult = await this.SendRequestInternal<WootradeAvailableTokens>(this.GetUri("token", true), HttpMethod.Get, CancellationToken.None);

            return new WebCallResult<WootradeAvailableTokens>(exchangeInfoResult.ResponseStatusCode,
                exchangeInfoResult.ResponseHeaders, exchangeInfoResult.Data, exchangeInfoResult.Error);
        }

        public async Task<WebCallResult<WootradeOrderBook>> GetOrderBookAsync(string symbol)
        {
            var exchangeInfoResult = await this.SendRequestInternal<WootradeOrderBook>(this.GetUri("orderbook", false, symbol), HttpMethod.Get, CancellationToken.None, null, true);

            if (exchangeInfoResult.Data is object)
                exchangeInfoResult.Data.Symbol = symbol;

            return new WebCallResult<WootradeOrderBook>(exchangeInfoResult.ResponseStatusCode,
                exchangeInfoResult.ResponseHeaders, exchangeInfoResult.Data, exchangeInfoResult.Error);
        }

        public async Task<WebCallResult<WootradeMarketTrades>> GetRecentTradesAsync(string symbol)
        {
            var parameters = new Dictionary<string, object>();

            parameters.Add("symbol", symbol);

            var tradesResult = await this.SendRequestInternal<WootradeMarketTrades>(
                this.GetUri("market_trades", true),
                HttpMethod.Get,
                CancellationToken.None,
                parameters
            );

            return WebCallResult<WootradeMarketTrades>.CreateFrom(tradesResult);
        }

        public async Task<WebCallResult<WootradeSymbolInfo>> GetSymbolAsync(string symbol)
        {
            var exchangeInfoResult = await this.SendRequestInternal<WootradeSymbolInfo>(this.GetUri("info", true, symbol), HttpMethod.Get, CancellationToken.None);

            return new WebCallResult<WootradeSymbolInfo>(exchangeInfoResult.ResponseStatusCode,
                exchangeInfoResult.ResponseHeaders, exchangeInfoResult.Data, exchangeInfoResult.Error);
        }

        public Task<WebCallResult<WootradeExchangeInfo>> GetSymbolsAsync()
        {
            return this.GetSymbolsAsync(CancellationToken.None);
        }

        public async Task<WebCallResult<WootradeExchangeInfo>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var exchangeInfoResult = await this.SendRequestInternal<WootradeExchangeInfo>(this.GetUri("info", true), HttpMethod.Get, ct);

            return new WebCallResult<WootradeExchangeInfo>(exchangeInfoResult.ResponseStatusCode,
                exchangeInfoResult.ResponseHeaders, exchangeInfoResult.Data, exchangeInfoResult.Error);
        }

        internal Uri GetUri(string endpoint, bool isPublic, string resourceId = "")
        {
            string result;

            if (isPublic)
                result = $"{BaseAddress}{apiVersion}/{publicPath}/{endpoint}/{resourceId}";
            else
                result = $"{BaseAddress}{apiVersion}/{endpoint}/{resourceId}";

            return new Uri(result);
        }

        internal Task<WebCallResult<T>> SendRequestInternal<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken,
          Dictionary<string, object>? parameters = null, bool signed = false, bool checkResult = true, PostParameters? postPosition = null, ArrayParametersSerialization? arraySerialization = null) where T : class
        {
            return base.SendRequest<T>(uri, method, cancellationToken, parameters, signed, checkResult, postPosition);
        }
    }
}