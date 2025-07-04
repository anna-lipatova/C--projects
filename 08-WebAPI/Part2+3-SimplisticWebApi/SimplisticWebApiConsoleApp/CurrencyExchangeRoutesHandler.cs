
public class CurrencyExchangeRoutesHandler : ISimplisticRoutesHandler {
    public void RegisterRoutes(RouteMap routeMap) {
        Console.WriteLine("+++ CurrencyExchangeRoutesHandler.RegisterRoutes() called.");

        // TODO: Register relevant routes in the routeMap
        routeMap.Map("/v2/currencies", GetCoinBaseCurrencies);

        routeMap.Map("/v2/exchange-rates", GetCoinBaseCurrencyExchangeByCurrencyName);
    }

    // TODO: Add service methods implementing Coinbase service with example data
    //		 (stored in strong-typed objects - in similar manner as PostRoutesHandler does)

    // TODO: Add "currencies" method returning info about EUR, USD, CZK currencies
    public CoinBaseResponseRoot GetCoinBaseCurrencies()
    {
        return new CoinBaseResponseRoot { Data = new List<CoinBaseItem> { new CoinBaseItem("EUR", "Euro", "0.01"), new CoinBaseItem("USD", "United States Dollar", "0.01"), new CoinBaseItem("CZK", "Czech Koruna", "0.01") } };
    }

    // TODO: Add "exchange-rates" method, that is able to return exchange rates for EUR and for CZK
    public CoinBaseCurrencyResponse GetCoinBaseCurrencyExchangeByCurrencyName(string currency)
    {
        return currency switch
        {
            "EUR" => new CoinBaseCurrencyResponse
            {
                Data = new CurrencyRates { Currency = "EUR", Rates = new Dictionary<string, string> { { "CZK", "25.0578030433971691" } } }
            },
            "CZK" => new CoinBaseCurrencyResponse
            {
                Data = new CurrencyRates { Currency = "CZK", Rates = new Dictionary<string, string> { { "EUR", "0.0399077284735664" } } }
            },
            _ => new CoinBaseCurrencyResponse { Data = {}}
        } ;
    }
}

// TODO: Add additional types if necessary
public class CoinBaseResponseRoot
{
    public required List<CoinBaseItem> Data { get; set; }
}
public record CoinBaseItem(string id, string name, string min_size);

public class CoinBaseCurrencyResponse
{
    public CurrencyRates Data { get; set; }
}

public class CurrencyRates
{
    public required string Currency { get; set; }
    public required IDictionary<string, string> Rates { get; set; }
}
