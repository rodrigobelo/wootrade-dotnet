using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Wootrade.Model.AccountData;
using Wootrade.Model.MarketData;
using Wootrade.Model.SpotData;

namespace Wootrade.Interfaces
{
    public interface IWootradeRestClient
    {
        /// <summary>
        /// Cancel order by client order id
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="clientOrderId">The client_order_id that wish to cancel</param>
        /// <returns></returns>
        Task<WebCallResult<WootradeCancelOrderResponse>> CancelOrderByClientIdAsync(string symbol, int clientOrderId);

        /// <summary>
        /// Cancel order by wootrade order id
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="wootradeOrderId">Order id created by Wootrade API</param>
        /// <returns></returns>
        Task<WebCallResult<WootradeCancelOrderResponse>> CancelOrderByWootradeOrderIdAsync(string symbol, int wootradeOrderId);

        /// <summary>
        /// Cancel all orders for a symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Task<WebCallResult<WootradeCancelOrderResponse>> CancelOrdersBySymbolAsync(string symbol);

        /// <summary>
        /// Get account information
        /// </summary>
        /// <returns></returns>
        Task<WebCallResult<WootradeAccountInformation>> GetAccountInformation();

        /// <summary>
        /// Get available tokens that WooTrade supported, it need to use when you call get deposit
        /// address or withdraw api.
        /// </summary>
        /// <returns></returns>
        Task<WebCallResult<WootradeAvailableTokens>> GetAvailableTokensAsync();

        /// <summary>
        /// Holding summary of client
        /// </summary>
        /// <returns></returns>
        Task<WebCallResult<WootradeCurrentHolding>> GetCurrentHoldingAsync(bool all = false);

        /// <summary>
        /// SNAPSHOT of current orderbook. Price of asks/bids are in descending order
        /// </summary>
        /// <param name="symbol">Symbol that you want to query</param>
        /// <returns></returns>
        Task<WebCallResult<WootradeOrderBook>> GetOrderBookAsync(string symbol);

        /// <summary>
        /// Get specific order detail by client_order_id
        /// </summary>
        /// <param name="clientOrderId">customized order_id when placing order</param>
        /// <returns></returns>
        Task<WebCallResult<WootradeOrderInfo>> GetOrderByClientOrderIdAsync(int clientOrderId);

        /// <summary>
        /// Get order by Wootrade order id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<WebCallResult<WootradeOrderInfo>> GetOrderByWootradeOrderIdAsync(int orderId);

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

        /// <summary>
        /// Place order maker/taker, the order executed information will be update from websocket
        /// stream. will response immediately with an order created message
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<WebCallResult<WootradeOrderPlacedResponse>> PlaceOrderAsync(WootradePlaceOrder order, CancellationToken ct = default);
    }
}