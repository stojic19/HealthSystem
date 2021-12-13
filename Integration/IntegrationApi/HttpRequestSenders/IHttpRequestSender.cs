using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace IntegrationAPI.HttpRequestSenders
{
    public interface IHttpRequestSender
    {
        public IRestResponse Post(string url, object jsonContent);
        public IRestResponse Get(string url);
    }
}
