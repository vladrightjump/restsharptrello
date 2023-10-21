using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;

namespace RestSharpTest.Tests.Update;

public class UpdateBoardValidationTest : BaseTest
{
    [Test]
    [TestCaseSource(typeof(BordIdValidationArgumentsProvider))]
    public static async Task CheckUpdateBoardWithInvalidId(BoardIdValidationArgumentsHolder validationArguments)
    {
        var request = RequestWithAuth(BoardsEndpoints.UpdatedBoardUrl)
            .AddOrUpdateParameters(validationArguments.PathParams);
        var response =await _client.ExecutePutAsync(request);
        
        Assert.That(response.StatusCode, Is.EqualTo(validationArguments.StatusCode));
        Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
    }
    
    [Test]
    [TestCaseSource(typeof(AuthValidationArgumentsProvider))]
    public async Task CheckUpdateGetBoardWithInvalidAuth(AuthValidationArgumentHolder validationArguments)
    {
        var request = RequestWithoutAuth(BoardsEndpoints.UpdatedBoardUrl)
            .AddOrUpdateParameters(validationArguments.AuthParams)
            .AddUrlSegment("id", UrlParamValues.BoardIdToUpdate)
            .AddJsonBody(new Dictionary<string, string>{{"name", "Update Name"}});
        var response = await _client.ExecutePutAsync(request);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo(validationArguments.BordValidationErrorMessage));
    }

    [Test]
    public static async Task CheckUpBoardWithoudUserCredentials()
    {
        var request = RequestWithoutAuth(BoardsEndpoints.GetBoardUrl)
            .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
            .AddUrlSegment("id", UrlParamValues.ExistingBoardId)
            .AddJsonBody(new Dictionary<string, string> { { "name", "Updated Name" } });
        var response =await _client.ExecutePutAsync(request);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo("invalid token"));
    }
}