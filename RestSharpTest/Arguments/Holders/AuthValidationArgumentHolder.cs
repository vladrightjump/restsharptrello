using RestSharp;

namespace RestSharpTest.Arguments.Holders;

public class AuthValidationArgumentHolder
{
    public IEnumerable<Parameter> AuthParams { get; set; }
    public string BordErrorMessage { get; set; }
    public string CardErrorMessage { get; set; }
    public string BordValidationErrorMessage { get; set; }
    public string CardAuthMessage { get; set; }

}