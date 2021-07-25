﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Wootrade.Converters;
using Wootrade.Interfaces;
using Wootrade.Model.MarketData;
using Wootrade.Model.Shared;
using Wootrade.Model.Spot;
using Wootrade.Model.SpotData;

namespace Wootrade
{
    public class WootradeRestClient : RestClient, IWootradeRestClient
    {
        private const string defaultApiVersion = "v1";
        private const string publicPath = "public";

        public WootradeRestClient() : this(new WootradeClientOptions())
        { }

        public WootradeRestClient(WootradeClientOptions clientOptions)
            : base("Wootrade", clientOptions, clientOptions.ApiCredentials == null ? null : new WootradeAuthenticationProvider(clientOptions.ApiCredentials))
        {
            this.requestBodyFormat = RequestBodyFormat.FormData;
        }

        public async Task<WebCallResult<WootradeCancelOrderResponse>> CancelOrderAsync(string symbol, int clientOrderId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "client_order_id", clientOrderId.ToString() }
            };

            var result = await SendRequest<WootradeCancelOrderResponse>(this.GetUri("client", false), HttpMethod.Delete, CancellationToken.None, parameters, true).ConfigureAwait(false);

            return result;
        }

        public async Task<WebCallResult<WootradeAvailableTokens>> GetAvailableTokensAsync()
        {
            var result = await this.SendRequestInternal<WootradeAvailableTokens>(this.GetUri("token", true), HttpMethod.Get, CancellationToken.None);

            return new WebCallResult<WootradeAvailableTokens>(result.ResponseStatusCode,
                result.ResponseHeaders, result.Data, result.Error);
        }

        public async Task<WebCallResult<WootradeCurrentHolding>> GetCurrentHoldingAsync(bool all = false)
        {
            var parameters = new Dictionary<string, object>
            {
                { "all", all.ToString().ToLower() }
            };

            var result = await SendRequest<WootradeCurrentHolding>(this.GetUri("client/holding", false, "", "v2"), HttpMethod.Get, CancellationToken.None, parameters, true).ConfigureAwait(false);

            return result;
        }

        public async Task<WebCallResult<WootradeOrderInfo>> GetOrderAsync(int clientOrderId)
        {
            var result = await this.SendRequestInternal<WootradeOrderInfo>(this.GetUri("client/order", false, clientOrderId.ToString()), HttpMethod.Get, CancellationToken.None, null, true);

            return new WebCallResult<WootradeOrderInfo>(result.ResponseStatusCode,
                result.ResponseHeaders, result.Data, result.Error);
        }

        public async Task<WebCallResult<WootradeOrderBook>> GetOrderBookAsync(string symbol)
        {
            var result = await this.SendRequestInternal<WootradeOrderBook>(this.GetUri("orderbook", false, symbol), HttpMethod.Get, CancellationToken.None, null, true);

            if (result.Data is object)
                result.Data.Symbol = symbol;

            return new WebCallResult<WootradeOrderBook>(result.ResponseStatusCode,
                result.ResponseHeaders, result.Data, result.Error);
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
            var result = await this.SendRequestInternal<WootradeSymbolInfo>(this.GetUri("info", true, symbol), HttpMethod.Get, CancellationToken.None);

            return new WebCallResult<WootradeSymbolInfo>(result.ResponseStatusCode,
                result.ResponseHeaders, result.Data, result.Error);
        }

        public Task<WebCallResult<WootradeExchangeInfo>> GetSymbolsAsync()
        {
            return this.GetSymbolsAsync(CancellationToken.None);
        }

        public async Task<WebCallResult<WootradeExchangeInfo>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var result = await this.SendRequestInternal<WootradeExchangeInfo>(this.GetUri("info", true), HttpMethod.Get, ct);

            return new WebCallResult<WootradeExchangeInfo>(result.ResponseStatusCode,
                result.ResponseHeaders, result.Data, result.Error);
        }

        public async Task<WebCallResult<WootradeOrderPlacedResponse>> PlaceOrderAsync(WootradePlaceOrder order, CancellationToken ct = default)
        {
            order.ThrowValidationPlaceOrder();

            var parameters = new Dictionary<string, object>
            {
                { "symbol", order.Symbol },
                { "side", JsonConvert.SerializeObject(order.Side, new OrderSideConverter(false)) },
                { "order_type", JsonConvert.SerializeObject(order.Type, new OrderTypeConverter(false)) }
            };

            parameters.AddOptionalParameter("order_quantity", order.Quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("order_amount", order.Amount?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("visible_quantity", order.VisibleQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("order_price", order.Price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("client_order_id", order.ClientOrderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("order_tag", order.Tag);

            var result = await SendRequest<WootradeOrderPlacedResponse>(this.GetUri("order", false), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);

            if (!result.Success && result.Error is object)
            {
                result.Error.Data = JsonConvert.DeserializeObject<WootradeRestError>(result.Error.Message);
            }

            return result;
        }

        internal Uri GetUri(string endpoint, bool isPublic, string resourceId = "", string apiVersion = "")
        {
            string result;

            if (string.IsNullOrEmpty(apiVersion))
            {
                apiVersion = defaultApiVersion;
            }

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