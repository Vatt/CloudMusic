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

        public static async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return await _client.SendAsync(request);
        }
    }
}
