using Newtonsoft.Json.Schema;
using RestSharp;
using RestSharpTest.Consts;

namespace RestSharpTest.Tests;

public class BaseTest
{
    
    protected static RestClient _client;

    [OneTimeSetUp]
    public static void InitializeRestClient()
    {
        _client = new RestClient("https://api.trello.com");
    }
    
    protected static RestRequest RequestWithAuth(string resource)
    {
        var request = RequestWithoutAuth(resource)
            .AddOrUpdateParameters(UrlParamValues.AuthQueryParams);
        return request;
    }
    
    protected static RestRequest RequestWithoutAuth(string resource)
    {
        var request = new RestRequest(resource);
        return request;
    }

    protected static JSchema GetSchema(string path ,string file)
    {
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), $"Resources/Schemas/{path}");
        var jsonSchemaPath = Path.Combine(directoryPath, file);
        return JSchema.Parse(File.ReadAllText(jsonSchemaPath));
                
    }
}