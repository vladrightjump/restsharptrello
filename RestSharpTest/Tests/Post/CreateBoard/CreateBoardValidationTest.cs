using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Arguments.Providers;
using RestSharpTest.Consts;

namespace RestSharpTest.Tests.Post;

public class CreateBoardValidationTest : BaseTest
{
    [Test]
    [TestCaseSource(typeof(BoardNameValidationArgumentsProvider))]
    public static async Task CheckCreateBoardWithInvalidName (IDictionary<string, object> bodyParams)
    {
        var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl)
            .AddJsonBody(bodyParams, ContentType.Json);
        var response =await _client.ExecutePostAsync(request);
        var message = JToken.Parse(response.Content).SelectToken("message").ToString();
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(message,Is.EqualTo("invalid value for name"));
    }

}