using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;

namespace RestSharpTest.Tests.Update;

public class UpdateBoardTest : BaseTest
{
    [Test]
    public static async Task CheckUpdateBoard()
    {
        var updatedName = "Update Name" + DateTime.Now.ToLongTimeString();
        var request = RequestWithAuth(BoardsEndpoints.UpdatedBoardUrl)
            .AddUrlSegment("id", UrlParamValues.BoardIdToUpdate)
            .AddJsonBody(new Dictionary<string, string>{{"name", updatedName}});
        var response = await _client.ExecutePutAsync(request);
        var responseContent = JToken.Parse(response.Content);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(responseContent.SelectToken("name").ToString(), Is.EqualTo(updatedName));
        request = RequestWithAuth(BoardsEndpoints.GetBoardUrl)
            .AddUrlSegment("id", UrlParamValues.BoardIdToUpdate);

        response = await _client.ExecuteGetAsync(request);
        responseContent = JToken.Parse(response.Content);
        Assert.AreEqual(updatedName, responseContent.SelectToken("name").ToString());

    }
}