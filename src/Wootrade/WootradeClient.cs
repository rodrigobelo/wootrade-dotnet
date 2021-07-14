using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;

namespace Wootrade
{
    public class WootradeClient : IExchangeClient
    {
        public Task<WebCallResult<ICommonOrderId>> CancelOrderAsync(string orderId, string symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonBalance>>> GetBalancesAsync(string accountId = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonOrder>>> GetClosedOrdersAsync(string symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonKline>>> GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonOrder>>> GetOpenOrdersAsync(string symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<ICommonOrder>> GetOrderAsync(string orderId, string symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<ICommonOrderBook>> GetOrderBookAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonRecentTrade>>> GetRecentTradesAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        public string GetSymbolName(string baseAsset, string quoteAsset)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonSymbol>>> GetSymbolsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<ICommonTicker>> GetTickerAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonTicker>>> GetTickersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<ICommonTrade>>> GetTradesAsync(string orderId, string symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<ICommonOrderId>> PlaceOrderAsync(string symbol, IExchangeClient.OrderSide side, IExchangeClient.OrderType type, decimal quantity, decimal? price = null, string accountId = null)
        {
            throw new NotImplementedException();
        }
    }
}