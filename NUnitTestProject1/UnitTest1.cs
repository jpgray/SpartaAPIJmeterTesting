using FluentAssertions;
using NUnit.Framework;
using RestSharp;

namespace NUnitTestProject1
{
    public class Tests
    {
        private RestClient _client;
        private RestRequest _request;
        private string FixerAPIKey = "50291ed68529014693bad5948b2ef70a";
        private string FixerBaseURL = "http://data.fixer.io/api/";

        [SetUp]
        public void Setup()
        {
            _client = new RestClient(FixerBaseURL);
            _request = new RestRequest();
        }

        [Test]
        public void LatestHealthCheck()
        {
            _request.Resource = "latest/{apiKey}";
            _request.AddUrlSegment("apiKey", FixerAPIKey);
            var response = _client.Execute(_request);
            response.StatusCode.ToString().Should().Be("OK");
        }
    }
}