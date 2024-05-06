using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Web;

class Program {
    static RequestProcessor requestProcessor = new RequestProcessor();

    static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        requestProcessor.RegisterAllRoutes();

        app.Run(ProcessRequest);
        app.UseHttpsRedirection();

        app.Run();
    }

    static Task ProcessRequest(HttpContext context) {
        var path = context.Request.Path.Value ?? "";
        var query = context.Request.QueryString.Value ?? "";
        var result = requestProcessor.HandleRequest(path, query);
        return context.Response.WriteAsync(result);
    }
}

class RequestProcessor {
    private RouteMap _routes = new RouteMap();

    public void RegisterAllRoutes() {
        // TODO: Find all classes implementing ISimplisticRoutesHandler in the calling assembly
        var currentAssembly = Assembly.GetExecutingAssembly();

        var typesImplementingInterface = currentAssembly.
            GetTypes().
            Where(type => typeof(ISimplisticRoutesHandler)
                                    .IsAssignableFrom(type) 
                                    && !type.IsInterface 
                                    && !type.IsAbstract); 

        // TODO: Create instance of each such class using parameter-less constructor
        // TODO: Call RegisterRoutes method on each such instance
        foreach(var type in typesImplementingInterface)
        {
            if (Activator.CreateInstance(type) is ISimplisticRoutesHandler handlerI)
            {
                handlerI.RegisterRoutes(_routes);
            }
        }
    }

	public string HandleRequest(string path, string query) {
        Console.WriteLine($"+++ Thread #{Thread.CurrentThread.ManagedThreadId} processing request:");
        Console.WriteLine($"    Path =\"{path}\"");
        Console.WriteLine($"    Query=\"{query}\" ...");

        // TODO: Pass correct arguments to requested service method (ignore additinal parameters in query, that the service method does not have)
        //       Parameter name matching should be case-insensitive!
        // TODO: Interpret %20 and + as space in query string
        var queryParams = HttpUtility.ParseQueryString(query);
        var parametersDictionary = queryParams.AllKeys.ToDictionary(k => k.ToLower(), k => queryParams[k]);

        // Поиск маршрута и соответствующего делегата
        if (_routes.TryGetHandler(path, out var routeHandler))
        {
            var methodParameters = routeHandler.Method.GetParameters();
            var arguments = new object[methodParameters.Length];
            bool missingParameter = false;

            for (int i = 0; i < methodParameters.Length; i++)
            {
                var parameter = methodParameters[i];
                if (parametersDictionary.TryGetValue(parameter.Name, out var value))
                {
                    arguments[i] = Convert.ChangeType(value, parameter.ParameterType);
                }
                else
                {
                    Console.WriteLine($"!!! Parameter {parameter.Name} is missing !!!");
                    missingParameter = true;
                }
            }

            if (!missingParameter)
            {
                var result = routeHandler.DynamicInvoke(arguments);
                return JsonSerializer.Serialize(result); 
            }
        }
        else
        {
            Console.WriteLine("!!! Route not found !!!");
        }

        return "{}";
    }
}

public class RouteMap {
    // TODO: Add your implementation here:
    //		 You need to add at least a suitable Map method here - see example usage in PostRoutesHandler.cs
    private Dictionary<string, Delegate> _routes = new Dictionary<string, Delegate>();
    public void Map(string path, Delegate d)
    {
        _routes[path.ToLower()] = d;
    }
    public bool TryGetHandler(string path, out Delegate d)
    {
        return _routes.TryGetValue(path.ToLower(), out d);
    }

}

// TODO: Add additional types if necessary

// IMPORTANT NOTE: You should NOT change anything in this interface
public interface ISimplisticRoutesHandler {
    public void RegisterRoutes(RouteMap routeMap);
} 
