using RestSharp;


namespace RestSharpTest.Consts;

public class UrlParamValues
{
    public const string ExistingBoardId = "650a79acfdb0b2b2517e5c77";
    public const string UserName = "me";
    public const string ValidKey = "7f9cad4066e9b4e921810882d7d0fc01";
    public const string ValidToken = "ATTAa6e2128ae73c22c88b2460e708691398d59b4b8425b044d6918568f451ed7c1b1866383B";
    public const string BoardIdToUpdate = "652040cf2ca93d01247b8170";

    public static readonly IEnumerable<Parameter> AuthQueryParams = new[]
    {
         Parameter.CreateParameter("key", ValidKey, ParameterType.QueryString),
        Parameter.CreateParameter("token", ValidToken, ParameterType.QueryString)
    };
    
    public static readonly IEnumerable<Parameter> AnotherUserAuthQueryParams = new[]
    {
        Parameter.CreateParameter("key", "7f9cad4066e9b4e921810882d7d0fc01", ParameterType.QueryString),
        Parameter.CreateParameter("token", "384rehfkodsjfpsaujf304u93u4932u4932yr98er39y493u49", ParameterType.QueryString)
    };

}