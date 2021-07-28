using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wootrade.Model.AccountData
{
    public class WootradeCurrentHolding
    {
        [JsonProperty("holding")]
        public IEnumerable<WootradeCurrentHoldingEntry> Holding { get; set; } = new List<WootradeCurrentHoldingEntry>();

        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}