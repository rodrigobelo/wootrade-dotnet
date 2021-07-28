# ![Icon](https://github.com/rodrigobelo/wootrade-dotnet/blob/main/wootrade-dotnet-icon.png?raw=true) Wootrade DotNet
A .NET Standard 2.0/2.1 Client for Wootrade API. 

## Features
This package is under construction, the following features are available:
- WebSockets Streams for Spot Market
- All public RESTful API endpoints available
- Authenticated OrderBook Snapshot endpoint available
- Important RESTful API endpoints for Orders available such as PlaceOrder, GetOrder, GetOrders and CancelOrder
- GetAccountInformation and GetCurrentHoldings endpoints available

If you find any issue or have questions, please please open an issue.

## Packages

|Name                             |nuget.org|
|---------------------------------|----|
|Wootrade.Net|[![Nuget Package](https://img.shields.io/nuget/v/Wootrade.Net.svg?logo=nuget)](https://www.nuget.org/packages/Wootrade.Net/) ![Nuget downloads](https://img.shields.io/nuget/dt/Wootrade.Net.svg)


## CryptoExchange.Net
Implementation is build upon the CryptoExchange.Net library, make sure to also check out the documentation on that: [docs](https://github.com/JKorf/CryptoExchange.Net)

Other CryptoExchange.Net implementations:
<table>
<tr>
<td><a href="https://github.com/JKorf/Bitfinex.Net"><img src="https://github.com/JKorf/Bitfinex.Net/blob/master/Bitfinex.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Bitfinex.Net">Bitfinex</a>
</td>
<td><a href="https://github.com/JKorf/Bittrex.Net"><img src="https://github.com/JKorf/Bittrex.Net/blob/master/Bittrex.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Bittrex.Net">Bittrex</a>
</td>
<td><a href="https://github.com/JKorf/Binance.Net"><img src="https://github.com/JKorf/Binance.Net/blob/master/Binance.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Binance.Net">Binance</a>
</td>
<td><a href="https://github.com/JKorf/CoinEx.Net"><img src="https://github.com/JKorf/CoinEx.Net/blob/master/CoinEx.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/CoinEx.Net">CoinEx</a>
</td>
<td><a href="https://github.com/JKorf/Huobi.Net"><img src="https://github.com/JKorf/Huobi.Net/blob/master/Huobi.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Huobi.Net">Huobi</a>
</td>
<td><a href="https://github.com/JKorf/Kucoin.Net"><img src="https://github.com/JKorf/Kucoin.Net/blob/master/Kucoin.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Kucoin.Net">Kucoin</a>
</td>
<td><a href="https://github.com/JKorf/Kraken.Net"><img src="https://github.com/JKorf/Kraken.Net/blob/master/Kraken.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Kraken.Net">Kraken</a>
</td>
<td><a href="https://github.com/Zaliro/Switcheo.Net"><img src="https://github.com/Zaliro/Switcheo.Net/blob/master/Resources/switcheo-coin.png?raw=true"></a>
<br />
<a href="https://github.com/Zaliro/Switcheo.Net">Switcheo</a>
</td>
<td><a href="https://github.com/ridicoulous/LiquidQuoine.Net"><img src="https://github.com/ridicoulous/LiquidQuoine.Net/blob/master/Resources/icon.png?raw=true"></a>
<br />
<a href="https://github.com/ridicoulous/LiquidQuoine.Net">Liquid</a>
</td>
<td><a href="https://github.com/burakoner/OKEx.Net"><img src="https://raw.githubusercontent.com/burakoner/OKEx.Net/master/Okex.Net/Icon/icon.png"></a>
<br />
<a href="https://github.com/burakoner/OKEx.Net">OKEx</a>
</td>
<td><a href="https://github.com/d-ugarov/Exante.Net"><img src="https://github.com/d-ugarov/Exante.Net/blob/master/Exante.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/d-ugarov/Exante.Net">Exante</a>
</td>
</tr>
</table>

## Donations
I develop and maintain this package on my own for free in my spare time. Donations are greatly appreciated.

**WOO**: 0x90badAB95512Ae586d376a5C64B7dF4f21C5e5cD

**ETH**: 0x90badAB95512Ae586d376a5C64B7dF4f21C5e5cD

## Getting started
Use your VS Package Manager Console to run the following command:

```
pm> Install-Package Wootrade.Net
```

## Wootrade RESTful Methods

The Wootrade API provides several RESTful methods. Some of them are public, some requires authentication.

### Public endpoints
````csharp
IWootradeRestClient client = new WootradeRestClient();

var symbols = await client.GetSymbolsAsync();
````

### Authenticated endpoints

#### First setup the RestClient for all authenticated calls
````csharp
WootradeClientOptions clientOptions = new WootradeClientOptions();
clientOptions.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(apiKey, apiSecret);

IWootradeRestClient client = new WootradeRestClient(clientOptions);
````

#### Example: Get current order book status
````csharp
var orderbook = await client.GetOrderBookAsync("SPOT_ETH_USDT");
````

#### Example: Place order

Look how is easy to place an order:

````csharp
 WootradePlaceOrder order = new WootradePlaceOrder();

order.ClientOrderId = 1;
order.Price = 0.4m;
order.Quantity = 15;
order.Side = OrderSide.Buy;
order.Symbol = "SPOT_WOO_USDT";
order.Tag = "AnyTagYouWant";
order.Type = OrderType.Limit;

var result = await client.PlaceOrderAsync(order);
````

## WebSockets Streams

The Wootrade API provides several socket endpoints. 
````csharp
WootradeSocketClientOptions clientOptions = new WootradeSocketClientOptions("applicationId");

IWootradeSocketClient client = new WootradeSocketClient(clientOptions);

await client.Spot.SubscribeToKlineUpdatesAsync("SPOT_BTC_USDT", KlineInterval.OneMinute, (data) => {
        // Handle data
    }
);
````

## Contributing

Contributions are more than welcome! Submit comments, issues or pull requests, I promise I'll check it out :)

While I try to do the best I can, suggestions/contributions are deeply appreciated on documentation!
