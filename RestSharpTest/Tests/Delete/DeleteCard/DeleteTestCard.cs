using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;

namespace RestSharpTest.Tests.Delete;

public class DeleteTestCard : BaseTest
{
    private static string _createdCadrId;

    [SetUp]
    public static async Task CreateCard()
    {
        var request = RequestWithAuth(CardsEndpoints.CreateCardUrl)
            .AddJsonBody(new Dictionary<string, string>
            {
                { "name", "New Name" },
                { "idList", CardsUrlParamValues.ExistingListId }
            });
        var response = await _client.ExecutePostAsync(request);
        _createdCadrId = JToken.Parse(response.Content).SelectToken("id").ToString();
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public static async Task CheckTheDeleteEndpoint()
    {
        var request = RequestWithAuth(CardsEndpoints.DeleteCardUrl).AddUrlSegment("id", _createdCadrId);
        var response = await _client.DeleteAsync(request);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(JToken.Parse(response.Content).SelectToken("_value"), Is.EqualTo(null));

        request = RequestWithAuth(CardsEndpoints.GetCardsUrl)
            .AddUrlSegment("list_id", CardsUrlParamValues.ExistingListId);
        response = await _client.ExecuteGetAsync(request);
        var responseContent = JToken.Parse(response.Content);
        Assert.False(responseContent.Children().Select(token => token.SelectToken("id").ToString()).Contains(_createdCadrId));

    }
}