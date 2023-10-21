using System.Net;
using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;

namespace RestSharpTest.Tests.Delete;

public class DeleteBoardValidationTest : BaseTest
{
    [Test]
    [TestCaseSource(typeof(BordIdValidationArgumentsProvider))]
    public static async Task CheckDeleteBoardWithInvalidId(BoardIdValidationArgumentsHolder validationArguments)
    {
        var request = RequestWithAuth(BoardsEndpoints.DeleteBoardUrl)
            .AddOrUpdateParameters(validationArguments.PathParams);
        var response =await _client.DeleteAsync(request);
        Assert.That(response.StatusCode, Is.EqualTo(validationArguments.StatusCode));
        Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
    }

    [Test]
    [TestCaseSource(typeof(AuthValidationArgumentsProvider))]
    public static async Task CheckDeleteBoardWithInvalidAuth(AuthValidationArgumentHolder validationArguments)
    {
        var request = RequestWithoutAuth(BoardsEndpoints.DeleteBoardUrl)
            .AddUrlSegment("id", UrlParamValues.ExistingBoardId)
            .AddOrUpdateParameters(validationArguments.AuthParams);
        var response =await _client.DeleteAsync(request);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo(validationArguments.BordValidationErrorMessage));
    }

    [Test]
    public static async Task CheckDeleteBoardWithAnotherUserCredentials()
    {
        var request = RequestWithoutAuth(BoardsEndpoints.DeleteBoardUrl)
            .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
            .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
        var response = await _client.DeleteAsync(request);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo("invalid token"));
    }
}