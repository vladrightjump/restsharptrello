using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using RestSharpTest.Consts;

namespace RestSharpTest.Tests.Get.GetCardTets 
{
    public class GetBoardTest : BaseTest
    {
        [TestFixture]
        public class GetBoardsTest
        {
            [Test]
            public static async Task CheckGetCards()
            {
                var request = RequestWithAuth(CardsEndpoints.GetCardsUrl)
                    .AddUrlSegment("list_id", CardsUrlParamValues.ExistingListId);
                var response = await _client.ExecuteGetAsync(request);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                var responseContent = JToken.Parse(response.Content);
                var jsonSchema = GetSchema("GetCard","get_cards.json");
                Assert.True(responseContent.IsValid(jsonSchema));
            }

            [Test]
            public static async Task CheckGetCard()
            {
                var request = RequestWithAuth(CardsEndpoints.GetCardUrl)
                    .AddUrlSegment("id", CardsUrlParamValues.ExistingCadId);
                var response = await _client.ExecuteGetAsync(request);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                string actualName = JToken.Parse(response.Content)["name"].ToString();

                Assert.AreEqual(CardsUrlParamValues.ExpectedCardName, actualName);
                var responseContent = JToken.Parse(response.Content);
                var jsonSchema = GetSchema("GetCard","get_card.json");
                Assert.True(responseContent.IsValid(jsonSchema));
            }
        }
    }
}