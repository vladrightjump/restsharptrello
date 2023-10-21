using System.Net;
using RestSharp;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;

namespace RestSharpTest.Tests.Post;

public class CardBoardValidationTest : BaseTest
{
    [Test]
    [TestCaseSource(typeof(CardBodyValidationArgumentProvider))]
    public static async Task CheckCreateCardWithInvalidName(CardBodyValidationArgumentHolder validationArguments)
    {
        var request = RequestWithAuth(CardsEndpoints.CreateCardUrl)
            .AddJsonBody(validationArguments.BodyParams);
        var response =await _client.ExecutePostAsync(request);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
    }

    [Test]
    [TestCaseSource(typeof(AuthValidationArgumentsProvider))]
    public static async Task CheckCreateCardInvalidAuth(AuthValidationArgumentHolder validationArguments)
    {
        var request = RequestWithoutAuth(CardsEndpoints.CreateCardUrl)
            .AddOrUpdateParameters(validationArguments.AuthParams)
            .AddJsonBody(new Dictionary<string, string>()
            {
                { "name", "NewItem" },
                { "idList", CardsUrlParamValues.ExistingListId }
            });
        var response = await _client.ExecutePostAsync(request);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo(validationArguments.CardAuthMessage));
    }

    [Test]
    public static async Task CheckCreatedCardWithAnotherUserCredentials()
    {
        var request = RequestWithoutAuth(CardsEndpoints.CreateCardUrl)
            .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
            .AddJsonBody(new Dictionary<string, string>()
            {
                { "name", "New item" },
                { "idList", CardsUrlParamValues.ExistingListId }
            });
        var response =await _client.ExecutePostAsync(request);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        Assert.That(response.Content, Is.EqualTo("invalid token"));
    }
}