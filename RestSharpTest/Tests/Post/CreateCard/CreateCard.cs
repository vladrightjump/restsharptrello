using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;

namespace RestSharpTest.Tests.Post;

public class CreateCard : BaseTest
{
    private static string _createdCardId;
    [Test]
    public static async Task CheckCreateNewCard()
    {
        var cardName = "New Card" + DateTime.Now.ToLongTimeString();
        var request = RequestWithAuth(CardsEndpoints.CreateCardUrl)
            .AddJsonBody(new Dictionary<string,string>
            {
                {"name",cardName},
                {"idList", CardsUrlParamValues.ExistingListId}
            });
        var response = await _client.PostAsync(request);
        var responseContent = JToken.Parse(response.Content);
        _createdCardId = responseContent.SelectToken("id").ToString();
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(responseContent.SelectToken("name")?.ToString(), Is.EqualTo(cardName));
        
        
    }
    
    [TearDown]
    public static async Task DeleteBoard()
    {
        var request = RequestWithAuth(CardsEndpoints.DeleteCardUrl)
            .AddUrlSegment("id", _createdCardId);
        var response =await _client.DeleteAsync(request);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}