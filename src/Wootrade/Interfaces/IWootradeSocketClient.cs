using CryptoExchange.Net.Interfaces;
using Wootrade.SocketSubClients.Interfaces;

namespace Wootrade.Interfaces
{
    public interface IWootradeSocketClient : ISocketClient
    {
        IWootradeSocketClientSpot Spot { get; set; }
    }
}