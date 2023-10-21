using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using RestSharpTest.Consts;

namespace RestSharpTest.Tests.Get.GetBoardTests
{
    class GetBoardsTest : BaseTest
    {
        [Test]
        public static async Task CheckGetBoards()
        {
            var request = RequestWithAuth(BoardsEndpoints.GetBoardsUrl)
                .AddQueryParameter("field","id,name")
                .AddUrlSegment("member", UrlParamValues.UserName);

            var response = await _client.ExecuteGetAsync(request);
        
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = GetSchema("GetBoard","get_boards.json");

            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Test]
        public void CheckGetBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.GetBoardUrl)
                .AddQueryParameter("field","id,name")
                .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
            var response = _client.Get(request);
        
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var expectedName = "Learning Postman 10";
            var actualName = JToken.Parse(response.Content).SelectToken("name").ToString();
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = GetSchema("GetBoard","get_board.json");

            Assert.True(responseContent.IsValid(jsonSchema));

            Assert.AreEqual(expectedName, actualName);
        }
    }
}