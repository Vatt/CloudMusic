using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.ServiceCore
{
    class CloudHttpHelper
    {
        private static HttpClient _client;

        static CloudHttpHelper()
        {
            _client = new HttpClient();
        }

        public static Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return _client.SendAsync(request);
        }
    }
}
