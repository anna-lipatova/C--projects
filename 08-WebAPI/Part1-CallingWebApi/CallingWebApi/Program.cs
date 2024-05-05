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

//Console.WriteLine("+++ via HttpClient & JsonSerializer:");

//var httpClient = new HttpClient();
//var json = httpClient.GetStringAsync(address + "/services/PostCode/getDataAsJson?cityOrPart=Sokolov&nameStreet=Karla+Hynka+Machy").Result;
//Console.WriteLine(json);
//Console.WriteLine();


////.Deserialize<IReadOnlyList<T>> kvuli interfacu muzeme koukat na json jako na kolekce
//var items = JsonSerializer.Deserialize<IReadOnlyList<PostCodeItem>>(json);
//Console.WriteLine("Item 0:");
//Console.WriteLine(items![0]);
//Console.WriteLine();

//var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
//items = JsonSerializer.Deserialize<IReadOnlyList<PostCodeItem>>(json, options);
//Console.WriteLine("Item 0:");
//Console.WriteLine(items![0]);
//Console.WriteLine();

Console.WriteLine("+++ via Refit:");

//var postApi = RestService.For<ICeskaPostaWebApi>(address);

//items = postApi.GetPostCodesByCityAndStreetAsync("Praha", "Patkova").Result;
//PrintPostCodeItems(items);
//items = postApi.GetPostCodesByCityAndStreetAsync("Sokolov", "Karla Hynka Machy").Result;
//PrintPostCodeItems(items);
//items = postApi.GetPostCodesByCityAndStreetAsync("Praha", "Malostranske namesti").Result;
//PrintPostCodeItems(items);

//static void PrintPostCodeItems(IEnumerable<PostCodeItem> items)
//{
//    foreach (var item in items)
//    {
//        Console.WriteLine(item);
//    }
//    Console.WriteLine();
//}

// TODO: Add example using Coinbase API via Refit to fetch and print all currencies
// TODO: Add example using Coinbase API via Refit to fecht and print all exchange rates for:
//		 EUR
//       CZK
static void PrintCoinBaseItems(IEnumerable<CoinBaseItem> root)
{
    foreach (var item in root)
    {
        Console.WriteLine(item);
    }
    Console.WriteLine();
}

static void PrintCoinBaseCurrencyItems(CurrencyRates data)
{
    Console.WriteLine(data);

    Console.WriteLine($"Currency: {data.Currency}");
    foreach (var item in data.Rates)
    {
        Console.WriteLine(item);
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

//public record PostCodeItem(string NameCity, string NameStreet, string Name, string PostCode, string Number);

//public interface ICeskaPostaWebApi
//{
//    [Get("/services/PostCode/getDataAsJson?cityOrPart={city}&nameStreet={street}")]
//    Task<IReadOnlyList<PostCodeItem>> GetPostCodesByCityAndStreetAsync(string city, string street);
//}



// TODO: Add type(s) for Coinbase entities
public class CoinBaseResponseRoot
{
    public List<CoinBaseItem> Data { get; set; }
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
    public string Currency { get; set; }
    public IDictionary<string, string> Rates { get; set; }
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

