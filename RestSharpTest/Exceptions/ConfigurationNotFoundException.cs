namespace RestSharpTest.Exceptions;

public class ConfigurationNotFoundException : RestSharpExceptions
{
    public ConfigurationNotFoundException(string message) : base(message) 
    {
        
    }
}