using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApi.HttpRequestSenders.Implementation
{
    public class HttpRequestSender : IHttpRequestSender
    {
        private readonly RestClient _restClient;

        public HttpRequestSender()
        {
            _restClient = new RestClient();
        }
        public IRestResponse Post(string url, object jsonContent)
        {
            RestRequest restRequest = new RestRequest(url);
            restRequest.AddJsonBody(jsonContent);
            return _restClient.Post(restRequest);
        }

        public IRestResponse Get(string url)
        {
            RestRequest restRequest = new RestRequest(url);
            return _restClient.Get(restRequest);
        }
    }
}
