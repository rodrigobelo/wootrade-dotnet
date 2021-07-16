using System;
using CryptoExchange.Net.Authentication;
using Dawn;

namespace Wootrade
{
    internal class WootradeAuthenticationProvider : AuthenticationProvider
    {
        private ApiCredentials apiCredentials;

        public WootradeAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
            Guard.Argument(credentials).NotNull("No valid API credentials provided. Key/Secret needed.");

            this.apiCredentials = credentials;
        }

        public override string Sign(string toSign)
        {
            throw new NotImplementedException();
        }
    }
}