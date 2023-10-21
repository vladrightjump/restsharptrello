using System.Net;
using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;

namespace RestSharpTest.Tests.Delete.DeleteCard;

public class DeleteCardValidationTest : BaseTest
{
    [Test]
    [TestCaseSource(typeof(CardValidationArgumentsProvider))]
    public async Task CheckDeleteCardWithInvalidId(CardIdValidationArgumentsHolder validationArguments)
    {
        var request = RequestWithAuth(CardsEndpoints.DeleteCardUrl)
            .AddOrUpdateParameters(validationArguments.PathParams);
        var response =await _client.DeleteAsync(request);
        Assert.That(response.StatusCode, Is.EqualTo(validationArguments.StatusCode));
        Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
    }

    [Test]
    [TestCaseSource(typeof(AuthValidationArgumentsProvider))]
    public async Task  CheckDeleteCardWithInvalidAuth(AuthValidationArgumentHolder validationArguments)
    {
        var request = RequestWithoutAuth(CardsEndpoints.DeleteCardUrl)
            .AddOrUpdateParameters(validationArguments.AuthParams)
            .AddUrlSegment("id", CardsUrlParamValues.ExistingCadId);
        var response = await _client.DeleteAsync(request);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo(validationArguments.CardAuthMessage));
    }

    [Test]
    public async Task CheckDeleteCardWithAnotherUserCredentials()
    {
        var request = RequestWithoutAuth(CardsEndpoints.DeleteCardUrl)
            .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
            .AddUrlSegment("id", CardsUrlParamValues.ExistingCadId);
        var response = _client.Delete(request);
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.AreEqual("invalid token", response.Content);
    }
}