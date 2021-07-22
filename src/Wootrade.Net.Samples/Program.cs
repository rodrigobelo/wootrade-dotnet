using System;
using System.Threading.Tasks;
using Wootrade.Interfaces;
using Wootrade.Model.Enums;
using Wootrade.Model.Shared;
using Wootrade.Model.Spot;

namespace Wootrade.Net.Samples
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            await RunGetSymbols();

            await RunKlineUpdates();

            Console.ReadLine();
        }

        private static async Task RunGetSymbols()
        {
            WootradeClientOptions clientOptions = new WootradeClientOptions();

            clientOptions.LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug;

            IWootradeRestClient client = new WootradeRestClient(clientOptions);

            var result = await client.GetSymbolsAsync();

            foreach (var item in result.Data)
            {
                Console.WriteLine("Symbol:" + item.CommonName);
                Console.WriteLine("MinimumTradeSize:" + item.CommonMinimumTradeSize);
            }

            Console.WriteLine("------------");
        }

        private static async Task RunKlineUpdates()
        {
            WootradeSocketClientOptions clientOptions = new WootradeSocketClientOptions("applicationId");

            clientOptions.LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug;
            clientOptions.AutoReconnect = true;

            IWootradeSocketClient client = new WootradeSocketClient(clientOptions);

            await client.Spot.SubscribeToKlineUpdatesAsync("SPOT_BTC_USDT", KlineInterval.OneMinute, (data) =>
            {
                Console.WriteLine("Start:" + data.Data.StartTime);
                Console.WriteLine("End:" + data.Data.EndTime);
                Console.WriteLine("Topic: " + data.Topic);
                Console.WriteLine("Symbol:" + data.Data.Symbol);
                Console.WriteLine("High:" + data.Data.High);
                Console.WriteLine("Low:" + data.Data.Low);
                Console.WriteLine("Open:" + data.Data.Open);
                Console.WriteLine("Close:" + data.Data.Close);
                Console.WriteLine("Volume:" + data.Data.Volume);
                Console.WriteLine("Amount:" + data.Data.Amount);
                Console.WriteLine("-------------------");
            }
        );
        }
    }
}