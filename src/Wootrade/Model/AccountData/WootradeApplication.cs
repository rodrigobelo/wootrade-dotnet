using Newtonsoft.Json;

namespace Wootrade.Model.AccountData
{
    public class WootradeApplication
    {
        [JsonProperty("account")]
        public string Account { get; set; } = "";

        [JsonProperty("alias")]
        public string Alias { get; set; } = "";

        [JsonProperty("application_id")]
        public string ApplicationId { get; set; } = "";

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("interest_rate")]
        public decimal InterestRate { get; set; }

        [JsonProperty("leverage")]
        public decimal Leverage { get; set; }

        [JsonProperty("maker_fee_rate")]
        public decimal MakerFeeRate { get; set; }

        [JsonProperty("margin_mode")]
        public string MarginMode { get; set; } = "";

        [JsonProperty("otpauth")]
        public bool OtpAuth { get; set; }

        [JsonProperty("taker_fee_rate")]
        public decimal TakerFeeRate { get; set; }
    }
}