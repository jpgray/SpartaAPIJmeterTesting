using System;
using TechTalk.SpecFlow;
using RestSharp;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace SpartaAPIJmeterTesting
{
    [Binding]
    public class FixerAPITestsSteps
    {
        private RestClient _client;
        private RestRequest _request;
        private IRestResponse _response;
        private ITestOutputHelper _output;
        private string APIKey = "50291ed68529014693bad5948b2ef70a";
        private string fixerBaseUrl = "https://fixer.io/api/latest/";

        private FixerAPITestsSteps(ITestOutputHelper output)
        {
            _client = new RestClient(fixerBaseUrl);
            _request = new RestRequest();
            _output = output;
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
