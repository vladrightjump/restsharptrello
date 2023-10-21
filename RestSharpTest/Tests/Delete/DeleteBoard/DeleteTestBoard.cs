using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;

namespace RestSharpTest.Tests.Delete;

public class DeleteTestBoard : BaseTest
{
    private static string _createdBoardId;

    [SetUp]
    public static async Task CreateBoard()
    {
        var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl)
            .AddJsonBody(new Dictionary<string, string> { { "name", "New Board" } });

        var response =await _client.ExecutePostAsync(request);

        _createdBoardId = JToken.Parse(response.Content).SelectToken("id").ToString();
    }

    [Test]
    public static async Task CheckDeleteBoard()
    {
        var request = RequestWithAuth(BoardsEndpoints.DeleteBoardUrl)
            .AddUrlSegment("id", _createdBoardId);
        var response =await _client.DeleteAsync(request);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(JToken.Parse(response.Content).SelectToken("_value").ToString(), Is.EqualTo(string.Empty));
        
        request = RequestWithAuth(BoardsEndpoints.GetBoardsUrl)
            .AddQueryParameter("field","id,name")
            .AddUrlSegment("member", UrlParamValues.UserName);
             
        response = await _client.ExecuteGetAsync(request);
        var responseContent = JToken.Parse(response.Content);
        Assert.False(responseContent.Children().Select(token => token.SelectToken("id")).Contains(_createdBoardId));
        
    }
}