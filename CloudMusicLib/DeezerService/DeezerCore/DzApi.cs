using CloudMusicLib.ServiceCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.DeezerService.DeezerCore
{
    class DzApi
    {
        public static DeezerService DzService;
        private static Dictionary<DzApiEnum, string> ApiDictionary = new Dictionary<DzApiEnum, string>
        {
            { DzApiEnum.Authorization, "" },
        };
        public static async Task<string> GetAuthDataJsonAsync(object user, object password)
        {
            var authUrlStr = ApiDictionary[DzApiEnum.Authorization];
            var cliendId = DzService.ClientId;
            var secretId = DzService.SecretId;
            object[] formatArgs = { cliendId, secretId, user, password };
            string urlAuth = String.Format(authUrlStr, formatArgs);
            var reqAuth = new HttpRequestMessage(HttpMethod.Post, urlAuth);
            reqAuth.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseAuth = CloudHttpHelper.Send(reqAuth);
            return await responseAuth.Content.ReadAsStringAsync();
        }

        public static DzConnection GetServiceConnection() => DzService.Connection as DzConnection;
    }
}
