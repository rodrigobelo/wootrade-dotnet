using System;
using Wootrade.Model.Enums;

namespace Wootrade.Model
{
    public class KlineIntervalAdapter
    {
        public static string AdaptToString(KlineInterval klineInterval)
        {
            switch (klineInterval)
            {
                case KlineInterval.OneMinute:
                    return "1m";

                case KlineInterval.FiveMinutes:
                    return "5m";

                case KlineInterval.FifteenMinutes:
                    return "15m";

                case KlineInterval.ThirtyMinutes:
                    return "30m";

                case KlineInterval.OneHour:
                    return "1h";

                case KlineInterval.OneDay:
                    return "1d";

                case KlineInterval.OneWeek:
                    return "1w";

                case KlineInterval.OneMonth:
                    return "1M";

                default:
                    throw new ArgumentException($"The value {klineInterval} for KLineInterval is not supported", nameof(klineInterval));
            }
        }
    }
}