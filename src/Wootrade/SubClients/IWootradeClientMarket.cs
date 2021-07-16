using System;
using System.Collections.Generic;
using System.Threading;
using CryptoExchange.Net.Objects;
using Wootrade.Interfaces;
using Wootrade.Model.Enums;

namespace Wootrade.SubClients
{
    public interface IWootradeClientMarket
    {
        WebCallResult<IEnumerable<IWootradeKline>> GetKlines(string symbol, KlineInterval interval,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);
    }
}