using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;
using WebSocket4Net;

namespace Wootrade.Tests.Helpers
{
    public class TestSocket : IWebsocket
    {
        public event Action OnClose;

        public event Action<Exception> OnError;

        public event Action<string> OnMessage;

        public event Action OnOpen;

        public bool CanConnect { get; set; }
        public bool Connected { get; set; }
#pragma warning disable 0067
#pragma warning restore 0067
        public Func<byte[], string> DataInterpreterBytes { get; set; }
        public Func<string, string> DataInterpreterString { get; set; }
        public DateTime? DisconnectTime { get; set; }
        public int Id { get; }
        public bool IsClosed => !Connected;
        public bool IsOpen => Connected;
        public string Origin { get; set; }
        public bool PingConnection { get; set; }
        public TimeSpan PingInterval { get; set; }
        public bool Reconnecting { get; set; }
        public bool ShouldReconnect { get; set; }
        public WebSocketState SocketState { get; }
        public SslProtocols SSLProtocols { get; set; }
        public TimeSpan Timeout { get; set; }
        public string Url { get; }

        public Task Close()
        {
            Connected = false;
            return Task.FromResult(0);
        }

        public Task<bool> Connect()
        {
            Connected = CanConnect;
            return Task.FromResult(CanConnect);
        }

        public void Dispose()
        {
        }

        public void InvokeClose()
        {
            Connected = false;
            OnClose?.Invoke();
        }

        public void InvokeMessage(string data)
        {
            OnMessage?.Invoke(data);
        }

        public void InvokeMessage<T>(T data)
        {
            OnMessage?.Invoke(JsonConvert.SerializeObject(data));
        }

        public void InvokeOpen()
        {
            OnOpen?.Invoke();
        }

        public void Reset()
        {
        }

        public void Send(string data)
        {
            if (!Connected)
                throw new Exception("Socket not connected");
        }

        public void SetProxy(string host, int port)
        {
            throw new NotImplementedException();
        }
    }
}