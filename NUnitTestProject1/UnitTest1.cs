using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Reflection;

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
            _request.Resource = "latest/";
            _request.AddQueryParameter("access_key", FixerAPIKey);
            var response = _client.Execute(_request);
            response.StatusCode.ToString().Should().Be("OK");
        }

        private void AddQueryParameters(IList<string> names, IList<string> values)
        {
            for(int i = 0; i < names.Count; i++)
            {
                _request.AddQueryParameter(names[i], values[i]);
            }
        }

        //will fail, subscription does not have access to convert api
        [Test]
        [Ignore("Invalid Subscription Plan")]
        public void ConvertGBPtoJPY()
        {
            _request.Resource = "convert";
            AddQueryParameters(new List<string> { "access_key", "from", "to", "amount" }, new List<string> { FixerAPIKey,"GBP","JPY","25"});
            var response = _client.Execute<ConvertDTO>(_request);
            response.Data.success.Should().BeTrue();
        }

        [Test]
        public void LatestRatesResponse()
        {
            _request.Resource = "latest";
            _request.AddQueryParameter("access_key", FixerAPIKey);
            var response = _client.Execute<LatestDTO>(_request);
            response.Data.success.Should().BeTrue();
            foreach(var property in response.Data.rates.GetType().GetProperties())
            {
                Assert.Greater((double)property.GetValue(response.Data.rates),0.0);
            }
        }
    }
}