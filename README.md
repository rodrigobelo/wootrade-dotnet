# Wootrade DotNet
A .NET Standard 2.0/2.1 Client for Wootrade API. Only Spot Market is available for now.

If you find any issue or have questions, please please open an issue.

## Packages

|Name                             |nuget.org|
|---------------------------------|----|
|Wootrade.Net|[![Nuget Package](https://img.shields.io/nuget/v/Wootrade.Net.svg?logo=nuget)](https://www.nuget.org/packages/Wootrade.Net/)

## Donations
I develop and maintain this package on my own for free in my spare time. Donations are greatly appreciated.

**WOO**: 0x90badAB95512Ae586d376a5C64B7dF4f21C5e5cD

**ETH**: 0x90badAB95512Ae586d376a5C64B7dF4f21C5e5cD

## Installation
Use your VS Package Manager Console to run the following command:

```
pm> Install-Package Wootrade.Net
```


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
