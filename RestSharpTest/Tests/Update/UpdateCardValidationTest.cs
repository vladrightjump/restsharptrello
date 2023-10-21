using System.Net;
using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;

namespace RestSharpTest.Tests.Update;

public class UpdateCardValidationTest : BaseTest
{
    [Test]
    [TestCaseSource(typeof(CardValidationArgumentsProvider))]
    public static async Task CheckUpdateCardWithInvalidId(CardIdValidationArgumentsHolder validationArguments)
    {
        var request = RequestWithAuth(CardsEndpoints.UpdateCardUrl)
            .AddOrUpdateParameters(validationArguments.PathParams);

        var response =await _client.ExecutePutAsync(request);
        Assert.That(response.StatusCode, Is.EqualTo(validationArguments.StatusCode));
        Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
    }

    [Test]
    [TestCaseSource(typeof(AuthValidationArgumentsProvider))]
    public static async Task CheckUpdateCardWithInvalidAuth( AuthValidationArgumentHolder validationArguments)
    {
        var request = RequestWithoutAuth(CardsEndpoints.UpdateCardUrl)
            .AddOrUpdateParameters(validationArguments.AuthParams)
            .AddUrlSegment("id", CardsUrlParamValues.ExistingCadId);

        var response =await _client.ExecutePutAsync(request);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo(validationArguments.CardAuthMessage));
    }
    [Test]
    public static async Task CheckUpdateCardWithAuthAnotherUserCredentials()
    {
        var request = RequestWithoutAuth(CardsEndpoints.UpdateCardUrl)
            .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
            .AddUrlSegment("id", CardsUrlParamValues.ExistingCadId);
        var response =await _client.ExecutePutAsync(request);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo("invalid token"));
    }
}