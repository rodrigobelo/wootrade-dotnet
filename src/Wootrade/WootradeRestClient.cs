using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.ExchangeInterfaces;
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

        public WootradeRestClient(WootradeClientOptions clientOptions)
            : base("Wootrade", clientOptions, clientOptions.ApiCredentials == null ? null : new WootradeAuthenticationProvider(clientOptions.ApiCredentials))
        {
        }

        public Task<WebCallResult<IEnumerable<ICommonSymbol>>> GetSymbolsAsync()
        {
            return this.GetSymbolsAsync(CancellationToken.None);
        }

        public async Task<WebCallResult<IEnumerable<ICommonSymbol>>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var exchangeInfoResult = await this.SendRequestInternal<WootradeExchangeInfo>(this.GetUri("info", true), HttpMethod.Get, ct);

            return new WebCallResult<IEnumerable<ICommonSymbol>>(exchangeInfoResult.ResponseStatusCode,
                exchangeInfoResult.ResponseHeaders, exchangeInfoResult.Data?.Symbols, exchangeInfoResult.Error);
        }

        internal Uri GetUri(string endpoint, bool isPublic)
        {
            string result;

            if (isPublic)
                result = $"{BaseAddress}{apiVersion}/{publicPath}/{endpoint}/";
            else
                result = $"{BaseAddress}{apiVersion}/{endpoint}/";

            return new Uri(result);
        }

        internal Task<WebCallResult<T>> SendRequestInternal<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken,
          Dictionary<string, object>? parameters = null, bool signed = false, bool checkResult = true, PostParameters? postPosition = null, ArrayParametersSerialization? arraySerialization = null) where T : class
        {
            return base.SendRequest<T>(uri, method, cancellationToken, parameters, signed, checkResult, postPosition);
        }
    }
}