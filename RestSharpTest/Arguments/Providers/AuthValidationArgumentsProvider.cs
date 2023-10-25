using System.Collections;
using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Consts;
using RestSharpTest.Services;
using RestSharpTest.Tests;

namespace RestSharpTest.Arguments.Providers;

public class AuthValidationArgumentsProvider : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new Object[]
        {
            new AuthValidationArgumentHolder
            {
                AuthParams = ArraySegment<Parameter>.Empty,
                BordErrorMessage = "unauthorized permission requested",
                CardErrorMessage = "unauthorized card permission requested",
                CardAuthMessage = "invalid key",
                BordValidationErrorMessage = "invalid key"
                
            }
        };
        yield return new Object[]
        {
            new AuthValidationArgumentHolder
            {
                AuthParams = new []{Parameter.CreateParameter("key",TestCore.AuthData.ValidKey,ParameterType.QueryString)},
                BordErrorMessage = "unauthorized permission requested",
                CardErrorMessage = "unauthorized card permission requested",
                CardAuthMessage = "unauthorized card permission requested",
                BordValidationErrorMessage = "unauthorized permission requested"
                
            }
        };
        yield return new Object[]
        {
            new AuthValidationArgumentHolder
            {
                AuthParams = new []{Parameter.CreateParameter("token",TestCore.AuthData.ValidToken,ParameterType.QueryString)},
                BordErrorMessage = "invalid key",
                CardErrorMessage = "invalid key",
                CardAuthMessage = "invalid key",
                BordValidationErrorMessage = "invalid key"

            }
        };
    }
}