using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Dawn;

namespace Wootrade
{
    internal class WootradeAuthenticationProvider : AuthenticationProvider
    {
        private readonly HMACSHA256 encryptor;

        public WootradeAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
            Guard.Argument(credentials).NotNull("No valid API credentials provided. Key/Secret needed.");

            if (credentials.Secret is null)
                throw new ArgumentException("No valid API credentials provided. Secret needed.");

            encryptor = new HMACSHA256(Encoding.ASCII.GetBytes(credentials.Secret.GetString()));
        }

        public override Dictionary<string, string> AddAuthenticationToHeaders(string uri, HttpMethod method, Dictionary<string, object> parameters, bool signed, PostParameters postParameterPosition, ArrayParametersSerialization arraySerialization)
        {
            if (Credentials.Key == null)
                throw new ArgumentException("No valid API credentials provided. Key/Secret needed.");

            if (!signed)
                return new Dictionary<string, string>();

            string timestamp =
                DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds.ToString("#");

            string signData = GetDataToBeSigned(method, parameters, postParameterPosition, arraySerialization, timestamp);

            return new Dictionary<string, string>
            {
                {
                    "x-api-key", Credentials.Key.GetString()
                },
                {
                    "x-api-timestamp", timestamp
                },
                {
                    "x-api-signature", ByteToString(encryptor.ComputeHash(Encoding.UTF8.GetBytes(signData)))
                }
            };
        }

        public override Dictionary<string, object> AddAuthenticationToParameters(string uri, HttpMethod method, Dictionary<string, object> parameters, bool signed, PostParameters postParameterPosition, ArrayParametersSerialization arraySerialization)
        {
            return parameters;
        }

        public override string Sign(string toSign)
        {
            throw new NotImplementedException();
        }

        private static string GetDataToBeSigned(HttpMethod method, Dictionary<string, object> parameters, PostParameters postParameterPosition, ArrayParametersSerialization arraySerialization, string timestamp)
        {
            string signData;

            if (method == HttpMethod.Get || method == HttpMethod.Delete || (postParameterPosition == PostParameters.InUri))
            {
                signData = parameters.CreateParamString(true, arraySerialization);
            }
            else
            {
                var formData = HttpUtility.ParseQueryString(string.Empty);
                foreach (var kvp in parameters.OrderBy(p => p.Key))
                {
                    if (kvp.Value.GetType().IsArray)
                    {
                        var array = (Array)kvp.Value;
                        foreach (var value in array)
                            formData.Add(kvp.Key, value.ToString());
                    }
                    else
                        formData.Add(kvp.Key, kvp.Value.ToString());
                }
                signData = formData.ToString();
            }

            return signData + "|" + timestamp;
        }
    }
}