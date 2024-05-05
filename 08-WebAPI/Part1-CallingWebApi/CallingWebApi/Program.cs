using System.Text.Json;
using Refit;

const string DefaultAddress = "https://b2c.cpost.cz";

Console.Write($"Enter web API address ({DefaultAddress} is default): ");
string? address = Console.ReadLine();
if (string.IsNullOrEmpty(address)) {
    address = DefaultAddress;
}

Console.WriteLine("+++ via HttpClient & JsonSerializer:");

var httpClient = new HttpClient();
var json = httpClient.GetStringAsync(address + "/services/PostCode/getDataAsJson?cityOrPart=Sokolov&nameStreet=Karla+Hynka+Machy").Result;
Console.WriteLine(json);
Console.WriteLine();


//.Deserialize<IReadOnlyList<T>> kvuli interfacu muveme koukat na json jako na kolekce
var items = JsonSerializer.Deserialize<IReadOnlyList<PostCodeItem>>(json);
Console.WriteLine("Item 0:");
Console.WriteLine(items![0]);
Console.WriteLine();

var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
items = JsonSerializer.Deserialize<IReadOnlyList<PostCodeItem>>(json, options);
Console.WriteLine("Item 0:");
Console.WriteLine(items![0]);
Console.WriteLine();

Console.WriteLine("+++ via Refit:");

var postApi = RestService.For<ICeskaPostaWebApi>(address);

items = postApi.GetPostCodesByCityAndStreetAsync("Praha", "Patkova").Result;
PrintPostCodeItems(items);
items = postApi.GetPostCodesByCityAndStreetAsync("Sokolov", "Karla Hynka Machy").Result;
PrintPostCodeItems(items);
items = postApi.GetPostCodesByCityAndStreetAsync("Praha", "Malostranske namesti").Result;
PrintPostCodeItems(items);

static void PrintPostCodeItems(IEnumerable<PostCodeItem> items) {
    foreach (var item in items) {
        Console.WriteLine(item);
    }
    Console.WriteLine();
}

// TODO: Add example using Coinbase API via Refit to fetch and print all currencies
// TODO: Add example using Coinbase API via Refit to fecht and print all exchange rates for:
//		 EUR
//       CZK

public record PostCodeItem(string NameCity, string NameStreet, string Name, string PostCode, string Number);

public interface ICeskaPostaWebApi {
    [Get("/services/PostCode/getDataAsJson?cityOrPart={city}&nameStreet={street}")]
    Task<IReadOnlyList<PostCodeItem>> GetPostCodesByCityAndStreetAsync(string city, string street);
}

// TODO: Add type(s) for Coinbase entities

// TODO: Add interface for Coinbase methods:
// TODO: Add method for "currencies" method
// TODO: Add method for "exchange-rates" method