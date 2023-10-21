using System.Collections;
using RestSharpTest.Arguments.Holders;
using RestSharpTest.Consts;

namespace RestSharpTest.Arguments.Providers;

public class CardBodyValidationArgumentProvider : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
            new CardBodyValidationArgumentHolder
            {
                BodyParams = new Dictionary<string, object>()
                {
                    { "name", 123 },
                    { "idList", CardsUrlParamValues.ExistingListId }
                },
                ErrorMessage = "invalid value for name"
            }
        };
        yield return new object[]
        {
            new CardBodyValidationArgumentHolder
            {
                BodyParams = new Dictionary<string, object>()
                {
                    { "name", "new card" },
                },
                ErrorMessage = "invalid value for idList"
            }
        };
        yield return new object[]
        {
            new CardBodyValidationArgumentHolder
            {
                BodyParams = new Dictionary<string, object>()
                {
                    { "name", "New card" },
                    { "idList", "invalid" }
                },
                ErrorMessage = "invalid value for idList"
            }
        };
        
    }
    

}
