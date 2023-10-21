using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;

namespace RestSharpTest.Tests.Update;

public class UpdateCardTest : BaseTest
{
    [Test]
    public static async Task CheckUpdateCard()
    {
        var updatedName = "Updated Name" + DateTime.Now.ToLongTimeString();
        var request = RequestWithAuth(CardsEndpoints.UpdateCardUrl)
            .AddUrlSegment("id", CardsUrlParamValues.CardIdToUpdate)
            .AddJsonBody(new Dictionary<string, string> { { "name", updatedName } });
        
        var response =await _client.ExecutePutAsync(request);
        var responseContent = JToken.Parse(response.Content);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(responseContent.SelectToken("name").ToString(), Is.EqualTo(updatedName));

        request = RequestWithAuth(CardsEndpoints.GetCardUrl)
            .AddUrlSegment("id", CardsUrlParamValues.CardIdToUpdate);
        response =await _client.ExecuteGetAsync(request);
        responseContent = JToken.Parse(response.Content);
        
        Assert.AreEqual(updatedName,responseContent.SelectToken("name").ToString());


    }
}