using RestSharp;
using RestSharpTest.Exceptions;
using RestSharpTest.Tests;


namespace RestSharpTest.Consts;

public class UrlParamValues : TestCore
{
    public const string ExistingBoardId = "650a79acfdb0b2b2517e5c77";
    public const string UserName = "me";
    public const string BoardIdToUpdate = "652040cf2ca93d01247b8170";
    public readonly string Validtkey = AuthData.ValidKey;
    public readonly string ValidToken = AuthData.ValidToken;
    
    public static readonly IEnumerable<Parameter> AuthQueryParams = new[]
    {
         Parameter.CreateParameter("key", AuthData.ValidKey, ParameterType.QueryString),
        Parameter.CreateParameter("token",  AuthData.ValidToken, ParameterType.QueryString)
    };
    
    public static readonly IEnumerable<Parameter> AnotherUserAuthQueryParams = new[]
    {
        Parameter.CreateParameter("key", AuthData.ValidKey , ParameterType.QueryString),
        Parameter.CreateParameter("token", AuthData.ValidToken, ParameterType.QueryString)
    };

}