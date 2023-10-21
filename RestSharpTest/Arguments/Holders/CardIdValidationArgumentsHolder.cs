using System.Net;
using RestSharp;

namespace RestSharpTest.Arguments.Holders;

public class CardIdValidationArgumentsHolder
{
    public IEnumerable<Parameter>PathParams { get; set; }
    
    public string ErrorMessage { get; set; }
    
    public HttpStatusCode StatusCode { get; set; }
    
    public HttpStatusCode StatusCodeAuth { get; set; }
}