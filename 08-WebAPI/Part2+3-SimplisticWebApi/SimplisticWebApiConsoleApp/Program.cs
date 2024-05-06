using System;
using System.Linq;
using System.Reflection;

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
 
		// TODO: Replace the code below with your own request processing (do not remove the logging above):

		// TODO: Pass correct arguments to requested service method (ignore additinal parameters in query, that the service method does not have)
		//       Parameter name matching should be case-insensitive!
		// TODO: Interpret %20 and + as space in query string
		// TODO: Check all parameters for the service method are part of the query - if not print following (for specific parameter name):
        Console.WriteLine($"!!! Parameter ABCDE is missing !!!");
		// TODO: Check the route is valid - if not print the following:
        Console.WriteLine($"!!! Route not found !!!");
        
        return "{}";
    }
}

public class RouteMap {
	// TODO: Add your implementation here:
	//		 You need to add at least a suitable Map method here - see example usage in PostRoutesHandler.cs
}

// TODO: Add additional types if necessary

// IMPORTANT NOTE: You should NOT change anything in this interface
public interface ISimplisticRoutesHandler {
    public void RegisterRoutes(RouteMap routeMap);
} 
