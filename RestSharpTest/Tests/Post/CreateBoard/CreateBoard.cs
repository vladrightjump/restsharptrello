using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTest.Consts;

namespace RestSharpTest.Tests.Post
{
    public class CreateBoard : BaseTest
    {
        private static string _createdBoardId;
        
        [Test]
        public static async Task CheckCreateBoard()
        {
            var boardName = "New Board" + DateTime.Now.ToLongTimeString();
            var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl)
                .AddJsonBody(new Dictionary<string,string>{{"name",boardName}});
            var response = await _client.PostAsync(request);

            var responseContent = JToken.Parse(response.Content);
            _createdBoardId = responseContent.SelectToken("id").ToString();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseContent.SelectToken("name")?.ToString(), Is.EqualTo(boardName));
        
             request = RequestWithAuth(BoardsEndpoints.GetBoardsUrl)
                .AddQueryParameter("field","id,name")
                .AddUrlSegment("member", UrlParamValues.UserName);
             
             response = await _client.ExecuteGetAsync(request);
             responseContent = JToken.Parse(response.Content);
             Assert.True(responseContent.Children().Select(token => token.SelectToken("name")).Contains(boardName));
            
        }
        [TearDown]
        public static async Task DeleteBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.DeleteBoardUrl)
                .AddUrlSegment("id", _createdBoardId);
            var response =await _client.DeleteAsync(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}