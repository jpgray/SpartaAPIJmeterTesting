using System;
using TechTalk.SpecFlow;
using RestSharp;
using FluentAssertions;

namespace SpartaAPIJmeterTesting
{
    [Binding]
    public class FixerAPITestsSteps
    {
        private RestClient _client;
        private RestRequest _request;
        private IRestResponse _response;
        private string APIKey = "123";
        private string fixerBaseUrl = "https://fixer.io/api/latest/";

        private FixerAPITestsSteps()
        {
            _client = new RestClient(fixerBaseUrl);
            _request = new RestRequest();
        }

        [When(@"I perform a health check")]
        public void WhenIPerformAHealthCheck()
        {
            _request.Method = Method.GET;
            _response = _client.Execute(_request);
        }
        
        [Then(@"I should see a status of '(.*)'")]
        public void ThenIShouldSeeAStatusOf(string p0)
        {
            _response.StatusCode.Should().Equals("OK");
        }

        [Given(@"I have a valid API Key")]
        public void GivenIHaveAValidAPIKey()
        {
            _request.Resource = "latest/{ApiKey}";
            _request.AddUrlSegment("ApiKey", APIKey);
        }
    }
}
