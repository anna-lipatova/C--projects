using System;
using System.Collections.Generic;

using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Refit;

//const string DefaultAddress = "https://b2c.cpost.cz";
//must be entered
const string DefaultAddress = "https://api.coinbase.com";

Console.Write($"Enter web API address ({DefaultAddress} is default): ");
string? address = Console.ReadLine();
if (string.IsNullOrEmpty(address)) {
    address = DefaultAddress;
}

// TODO: Add example using Coinbase API via Refit to fetch and print all currencies
// TODO: Add example using Coinbase API via Refit to fecht and print all exchange rates for:
//		 EUR
//       CZK
static void PrintCoinBaseItems(IEnumerable<CoinBaseItem> root)
{
    Console.WriteLine();
    Console.WriteLine("+++ Fetching and printing all currencies: +++" );
    foreach (var item in root)
    {
        Console.WriteLine(item);
    }
    Console.WriteLine();
}

static void PrintCoinBaseCurrencyItems(CurrencyRates data)
{
    Console.WriteLine();
    Console.WriteLine($"+++ Fetching and printing exchange rates for: {data.Currency} +++");

    foreach (var rate in data.Rates)
    {
        Console.WriteLine($"Currency: {rate.Key}, Rate: {rate.Value}");
    }
    Console.WriteLine();
}

var coinBaseApi = RestService.For<ICoinBaseWebApi>(address);

var coinResponse = coinBaseApi.GetCoinBaseCurrenciesAsync().Result;
PrintCoinBaseItems(coinResponse.Data);

var coinCurrencyResponse = coinBaseApi.GetCoinBaseExchangeRatesByCurrencyAsync("EUR").Result;
PrintCoinBaseCurrencyItems(coinCurrencyResponse.Data);

coinCurrencyResponse = coinBaseApi.GetCoinBaseExchangeRatesByCurrencyAsync("CZK").Result;
PrintCoinBaseCurrencyItems(coinCurrencyResponse.Data);

// TODO: Add type(s) for Coinbase entities
public class CoinBaseResponseRoot
{
    public required List<CoinBaseItem> Data { get; set; }
}

//without [JsonPropertyName("min_size")], must be in class
public record CoinBaseItem(string id, string name, string min_size);

//public class CoinBaseItem
//{
//    public string Id { get; set; }
//    public string Name { get; set; }

//    [JsonPropertyName("min_size")]
//    public string MinSize { get; set; }

//    public CoinBaseItem(string id, string name, string minSize)
//    {
//        Id = id;
//        Name = name;
//        MinSize = minSize;
//    }
//}

public class CoinBaseCurrencyResponse
{
    public CurrencyRates Data {  get; set; }
}

public class CurrencyRates
{
    public required string Currency { get; set; }
    public required IDictionary<string, string> Rates { get; set; }
}

// TODO: Add interface for Coinbase methods:
public interface ICoinBaseWebApi
{
    // TODO: Add method for "currencies" method
    [Get("/v2/currencies")]
    Task<CoinBaseResponseRoot> GetCoinBaseCurrenciesAsync();


    // TODO: Add method for "exchange-rates" method
    [Get("/v2/exchange-rates?currency={currency}")]
    Task<CoinBaseCurrencyResponse> GetCoinBaseExchangeRatesByCurrencyAsync(string currency);
}

