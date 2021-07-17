using System;
using Wootrade.Interfaces;
using Wootrade.Model.Enums;
using Wootrade.Model.MarketStream;
using Wootrade.Tests.Helpers;
using Xunit;

namespace Wootrade.Tests.Unit
{
    public class WootradeNetTestSuite
    {
        [Fact]
        public void SubscribingToKlineStream_Should_TriggerWhenKlineStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            IWootradeSocketClient client = TestHelpers.CreateSocketClient(socket);

            IWootradeStreamKlineData result = null;
            client.Spot.SubscribeToKlineUpdatesAsync("SPOT_BTC_USDT", KlineInterval.OneMinute, (test) => result = test);

            var data = new WootradeStreamKlineData()
            {
                Topic = "SPOT_BTC_USDT@kline_1m",
                Data = new WootradeStreamKline()
                {
                    Symbol = "SPOT_BTC_USDT",
                    Type = "1m",
                    StartTime = new DateTime(2017, 1, 1),
                    Close = 0.2m,
                    EndTime = new DateTime(2017, 1, 2),
                    High = 0.3m,
                    Low = 0.4m,
                    Open = 0.5m,
                    Volume = 0.8m
                }
            };

            // act
            socket.InvokeMessage(data);

            // assert
            Assert.NotNull(result);
            Assert.True(TestHelpers.AreEqual(data, result, "Data"));
            Assert.True(TestHelpers.AreEqual(data.Data, result.Data));
        }
    }
}