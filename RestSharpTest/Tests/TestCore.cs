using Microsoft.Extensions.Configuration;
using RestSharpTest.Exceptions;
using RestSharpTest.Options;
using RestSharpTest.Services;

namespace RestSharpTest.Tests;

public class TestCore : RestSharpExceptions
{
    public static IConfigurationRoot Configuration { get; private set; }
    public static ProjectSettings ProjectSettings { get; private set; }
    public static AuthParams AuthData { get; private set; }
    
    [OneTimeSetUp]
    public  void SetUpConfigurations()
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        
        Configuration = builder.Build();
        ProjectSettings = Configuration.GetSection("ProjectSettings").Get<ProjectSettings>();
        
        var validKey = Environment.GetEnvironmentVariable("ValidKey", EnvironmentVariableTarget.Process);
        var validToken = Environment.GetEnvironmentVariable("ValidToken", EnvironmentVariableTarget.Process);

        if (string.IsNullOrEmpty(validKey))
            throw new ConfigurationNotFoundException("Environment Variable ValidKey not found");

        if (string.IsNullOrEmpty(validToken))
            throw new ConfigurationNotFoundException("Environment Variable ValidToken not found");

        AuthData = new AuthParams()
        {
            ValidKey = validKey,
            ValidToken = validToken
        };
    }
}