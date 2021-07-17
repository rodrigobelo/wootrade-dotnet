﻿using CryptoExchange.Net.Objects;

namespace Wootrade.Model.Spot
{
    public class WootradeSocketClientOptions : SocketClientOptions
    {
        public WootradeSocketClientOptions(string applicationId, string baseAddress) : base(baseAddress)
        {
            ApplicationId = applicationId;
        }

        public WootradeSocketClientOptions(string applicationId) : base("wss://wss.staging.woo.network/ws/stream/" + applicationId)
        {
            ApplicationId = applicationId;
        }

        public string ApplicationId { get; set; }
    }
}