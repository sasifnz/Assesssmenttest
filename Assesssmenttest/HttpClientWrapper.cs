using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Assesssmenttest
{
    public static class HttpClientWrapper
    {
        private static string _apiBaseUrl = "https://jsonplaceholder.typicode.com/";

        public static HttpClient HttpClientSetup()
        {
            var client = new HttpClient { BaseAddress = new Uri(_apiBaseUrl) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization = _authenticationHeader;

            return client;
        }

    }

}
