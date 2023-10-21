using System.Net;
using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;

namespace RestSharpTest.Tests.Get.GetBoardTests;

public class GetBoardsValidationsTests : BaseTest
{
    [Test]
    [TestCaseSource(typeof(BordIdValidationArgumentsProvider))]
    public async Task CheckGetBoardWithInvalidId(BoardIdValidationArgumentsHolder validationArguments)
    {
        var request = RequestWithoutAuth(BoardsEndpoints.GetBoardUrl)
            .AddOrUpdateParameters(validationArguments.PathParams);
        var response = await _client.ExecuteGetAsync(request);
        Assert.AreEqual(validationArguments.StatusCode, response.StatusCode);
        Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
    }
    
    [Test]
    [TestCaseSource(typeof(AuthValidationArgumentsProvider))]
    public async Task CheckGetBoardWithInvalidAuth(AuthValidationArgumentHolder validationArguments)
    {
        var request = RequestWithoutAuth(BoardsEndpoints.GetBoardUrl)
            .AddOrUpdateParameters(validationArguments.AuthParams)
            .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
        var response = await _client.ExecuteGetAsync(request);
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.AreEqual(validationArguments.BordErrorMessage, response.Content);
    }
    
    [Test]
    public async Task CheckGetBoardWithAnotherUSerCredentials()
    {
        var request = RequestWithoutAuth(BoardsEndpoints.GetBoardUrl)
            .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
            .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
        var response = await _client.ExecuteGetAsync(request);
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.AreEqual("invalid token", response.Content);
    }

}