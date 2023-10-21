using System.Net;
using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;

namespace RestSharpTest.Tests.Get.GetCardTets;

public class GetCardsValidationTests : BaseTest
{
    [Test]
    [TestCaseSource(typeof(CardValidationArgumentsProvider))]
    public async Task CheckGetCardWithInvalidId(CardIdValidationArgumentsHolder validationArguments)
    {
        var request = RequestWithAuth(CardsEndpoints.GetCardUrl)
            .AddOrUpdateParameters(validationArguments.PathParams);
        var response = await _client.ExecuteGetAsync(request);
        Assert.AreEqual(validationArguments.StatusCode, response.StatusCode);
        Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
    }
    
    [Test]
    [TestCaseSource(typeof(AuthValidationArgumentsProvider))]
    public async Task CheckGetCardWithInvalidAuth(AuthValidationArgumentHolder validationArguments)
    {
        var request = RequestWithoutAuth(CardsEndpoints.GetCardUrl)
            .AddOrUpdateParameters(validationArguments.AuthParams)
            .AddUrlSegment("id", CardsUrlParamValues.ExistingCadId);
        var response = await _client.ExecuteGetAsync(request);
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.AreEqual(validationArguments.CardErrorMessage, response.Content);
    }
    
    [Test]
    public async Task CheckGetCardWithAnotherUSerCredentials()
    {
        var request = RequestWithAuth(CardsEndpoints.GetCardUrl)
            .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
            .AddUrlSegment("id", CardsUrlParamValues.ExistingCadId);
        var response = await _client.ExecuteGetAsync(request);
        
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.AreEqual("invalid token", response.Content);
    }
}