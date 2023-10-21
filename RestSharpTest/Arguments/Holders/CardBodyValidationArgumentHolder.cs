namespace RestSharpTest.Arguments.Holders;

public class CardBodyValidationArgumentHolder
{
        public IDictionary<string,object> BodyParams { get; set; }
        
        public string ErrorMessage { get; set; }
}