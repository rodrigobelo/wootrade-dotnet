using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Wootrade.Converters;
using Wootrade.Interfaces;
using Wootrade.Model.AccountData;
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

        public async Task<WebCallResult<WootradeCancelOrderResponse>> CancelOrderByClientIdAsync(string symbol, int clientOrderId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "client_order_id", clientOrderId.ToString() }
            };

            var result = await SendRequest<WootradeCancelOrderResponse>(this.GetUri("client/order", false), HttpMethod.Delete, CancellationToken.None, parameters, true).ConfigureAwait(false);

            return result;
        }

        public async Task<WebCallResult<WootradeCancelOrderResponse>> CancelOrderByWootradeOrderIdAsync(string symbol, int wootradeOrderId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "order_id", wootradeOrderId.ToString() }
            };

            var result = await SendRequest<WootradeCancelOrderResponse>(this.GetUri("order", false), HttpMethod.Delete, CancellationToken.None, parameters, true, true, PostParameters.InBody).ConfigureAwait(false);

            return result;
        }

        public async Task<WebCallResult<WootradeCancelOrderResponse>> CancelOrdersBySymbolAsync(string symbol)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            var result = await SendRequest<WootradeCancelOrderResponse>(this.GetUri("orders", false), HttpMethod.Delete, CancellationToken.None, parameters, true).ConfigureAwait(false);

            return result;
        }

        public async Task<WebCallResult<WootradeAccountInformation>> GetAccountInformation()
        {
            var result = await this.SendRequestInternal<WootradeAccountInformation>(this.GetUri("client/info", false), HttpMethod.Get, CancellationToken.None, null, true);

            return new WebCallResult<WootradeAccountInformation>(result.ResponseStatusCode,
                result.ResponseHeaders, result.Data, result.Error);
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

        public async Task<WebCallResult<WootradeOrderBook>> GetOrderBookAsync(string symbol)
        {
            var result = await this.SendRequestInternal<WootradeOrderBook>(this.GetUri("orderbook", false, symbol), HttpMethod.Get, CancellationToken.None, null, true);

            if (result.Data is object)
                result.Data.Symbol = symbol;

            return new WebCallResult<WootradeOrderBook>(result.ResponseStatusCode,
                result.ResponseHeaders, result.Data, result.Error);
        }

        public async Task<WebCallResult<WootradeOrderInfo>> GetOrderByClientOrderIdAsync(int clientOrderId)
        {
            var result = await this.SendRequestInternal<WootradeOrderInfo>(this.GetUri("client/order", false, clientOrderId.ToString()), HttpMethod.Get, CancellationToken.None, null, true);

            return new WebCallResult<WootradeOrderInfo>(result.ResponseStatusCode,
                result.ResponseHeaders, result.Data, result.Error);
        }

        public async Task<WebCallResult<WootradeOrderInfo>> GetOrderByWootradeOrderIdAsync(int orderId)
        {
            var result = await this.SendRequestInternal<WootradeOrderInfo>(this.GetUri("order", false, orderId.ToString()), HttpMethod.Get, CancellationToken.None, null, true);

            return new WebCallResult<WootradeOrderInfo>(result.ResponseStatusCode,
                result.ResponseHeaders, result.Data, result.Error);
        }

        public async Task<WebCallResult<IEnumerable<WootradeOrderInfo>>> GetOrdersAsync(GetOrdersFilter filter)
        {
            var orders = new List<WootradeOrderInfo>();

            WebCallResult<WootradeGetOrdersPage>? lastResult;

            do
            {
                filter.Page++;

                var parameters = new Dictionary<string, object>();

                parameters.AddOptionalParameter("symbol", filter.Symbol);
                parameters.AddOptionalParameter("side", filter.Side?.ToString().ToUpper());
                parameters.AddOptionalParameter("order_type", filter.Type?.ToString().ToUpper());
                parameters.AddOptionalParameter("order_tag", filter.Tag);
                parameters.AddOptionalParameter("status", filter.Status?.ToString().ToUpper());
                parameters.AddOptionalParameter("start_t", this.GetDateInSeconds(filter.StartDate));
                parameters.AddOptionalParameter("end_t", this.GetDateInSeconds(filter.EndDate));
                parameters.AddOptionalParameter("page", filter.Page);

                lastResult = await SendRequest<WootradeGetOrdersPage>(this.GetUri("orders", false), HttpMethod.Get, CancellationToken.None, parameters, true).ConfigureAwait(false);

                if (lastResult.Success)
                {
                    orders.AddRange(lastResult.Data.Orders);
                }
            } while (lastResult.Success
                    && lastResult.Data.Success
                    && lastResult.Data.Orders.Any()
                    && lastResult.Data.Orders.Count() >= lastResult.Data.Meta.RecordsPerPage
            );

            return new WebCallResult<IEnumerable<WootradeOrderInfo>>(
                lastResult.ResponseStatusCode,
                lastResult.ResponseHeaders,
                orders,
                lastResult.Error);
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
            {
                if (string.IsNullOrEmpty(resourceId))
                {
                    result = $"{BaseAddress}{apiVersion}/{endpoint}";
                }
                else
                {
                    result = $"{BaseAddress}{apiVersion}/{endpoint}/{resourceId}";
                }
            }

            return new Uri(result);
        }

        internal Task<WebCallResult<T>> SendRequestInternal<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken,
          Dictionary<string, object>? parameters = null, bool signed = false, bool checkResult = true, PostParameters? postPosition = null, ArrayParametersSerialization? arraySerialization = null) where T : class
        {
            return base.SendRequest<T>(uri, method, cancellationToken, parameters, signed, checkResult, postPosition);
        }

        protected override IRequest ConstructRequest(Uri uri, HttpMethod method, Dictionary<string, object>? parameters, bool signed, PostParameters postPosition, ArrayParametersSerialization arraySerialization, int requestId)
        {
            if (method != HttpMethod.Delete)
                return base.ConstructRequest(uri, method, parameters, signed, postPosition, arraySerialization, requestId);
            else
            {
                if (parameters == null)
                    parameters = new Dictionary<string, object>();

                var uriString = uri.ToString();
                if (authProvider != null)
                    parameters = authProvider.AddAuthenticationToParameters(uriString, method, parameters, signed, postPosition, arraySerialization);

                var contentType = requestBodyFormat == RequestBodyFormat.Json ? Constants.JsonContentHeader : Constants.FormContentHeader;
                var request = RequestFactory.Create(method, uriString, requestId);
                request.Accept = Constants.JsonContentHeader;

                var headers = new Dictionary<string, string>();
                if (authProvider != null)
                    headers = authProvider.AddAuthenticationToHeaders(uriString, method, parameters!, signed, postPosition, arraySerialization);

                foreach (var header in headers)
                    request.AddHeader(header.Key, header.Value);

                if (parameters?.Any() == true)
                    WriteParamBody(request, parameters, contentType);

                return request;
            }
        }

        private object GetDateInSeconds(DateTime? value)
        {
            if (value is null)
                return null!;
            else
                return (long)Math.Round(((DateTime)value! - new DateTime(1970, 1, 1)).TotalMilliseconds);
        }

        private object GetEmptyIfNull(object obj)
        {
            if (obj is null)
                return string.Empty;

            return obj;
        }
    }
}