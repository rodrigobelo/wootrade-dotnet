using Newtonsoft.Json;

namespace Wootrade.Model.Shared
{
    public class WootradeRestError
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public string GetErrorName()
        {
            switch (Code)
            {
                case -1000:
                    return "UNKNOWN";

                case -1001:
                    return "INVALID_SIGNATURE";

                case -1002:
                    return "UNAUTHORIZED";

                case -1003:
                    return "TOO_MANY_REQUEST";

                case -1004:
                    return "UNKNOWN_PARAM";

                case -1005:
                    return "INVALID_PARAM";

                case -1006:
                    return "RESOURCE_NOT_FOUND";

                case -1007:
                    return "DUPLICATE_REQUEST";

                case -1008:
                    return "QUANTITY_TOO_HIGH";

                case -1009:
                    return "CAN_NOT_WITHDRAWAL";

                case -1011:
                    return "RPC_NOT_CONNECT";

                case -1012:
                    return "RPC_REJECT";

                case -1101:
                    return "RISK_TOO_HIGH";

                case -1102:
                    return "MIN_NOTIONAL";

                case -1103:
                    return "PRICE_FILTER";

                case -1104:
                    return "SIZE_FILTER";

                case -1105:
                    return "PERCENTAGE_FILTER";

                default:
                    return "UNKNOWN";
            }
        }
    }
}