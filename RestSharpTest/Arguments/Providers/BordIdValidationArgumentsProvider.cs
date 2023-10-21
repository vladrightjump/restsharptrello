using System.Collections;
using System.Net;
using RestSharp;
using RestSharpTest.Arguments.Holders;

namespace RestSharpTest.Arguments.Providers;

public class BordIdValidationArgumentsProvider : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
            new BoardIdValidationArgumentsHolder()
            {
                ErrorMessage = "invalid id",
                StatusCode = HttpStatusCode.BadRequest,
                PathParams = new[]
                {
                    Parameter.CreateParameter("id", "invalid", ParameterType.UrlSegment)
                }
            }
        };
        yield return new object[]
        {
            new BoardIdValidationArgumentsHolder()
            {
                ErrorMessage = "The requested resource was not found.",
                StatusCode = HttpStatusCode.NotFound,
                PathParams = new[]
                {
                    Parameter.CreateParameter("id", "650a79acfdb0b2b2517e5c71", ParameterType.UrlSegment)
                }
            }
        };
    }
}