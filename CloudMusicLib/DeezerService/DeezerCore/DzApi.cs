using CloudMusicLib.ServiceCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.DeezerService.DeezerCore
{//$"http://connect.deezer.com/oauth/auth.php?app_id={service.ClientId}&redirect_uri=https://connect.deezer.com/&response_type=token&perms=basic_access";
    class DzApi
    {
        public static DeezerService service;
        public static Dictionary<DzApiEnum, string> ApiDictionary = new Dictionary<DzApiEnum, string>
        {
            {   DzApiEnum.LoginUrlString, "http://connect.deezer.com/oauth/auth.php?app_id={0}&redirect_uri={1}&response_type=token&perms=basic_access,email " },
            {   DzApiEnum.MeInfo,         "http://api.deezer.com/user/me&access_token={0}" },
            {   DzApiEnum.TracksSearch,   "http://api.deezer.com/search/track?q={0}&access_token={1}"}
        };
        public static DzConnection GetServiceConnection() => service.Connection as DzConnection;
        public static string GetLoginUrlString()
        {
            return String.Format(ApiDictionary[DzApiEnum.LoginUrlString],service.ClientId,service.RedirectUri);
        }
        public static void Init(DeezerService dzService)
        {
            service = dzService;
        }
        public static async Task<string> GetUserInfoJson(string accessTok)
        {
            var meUrlStr = ApiDictionary[DzApiEnum.MeInfo];
            var UrlStr = String.Format(meUrlStr, accessTok);
            var reqMe = new HttpRequestMessage(HttpMethod.Get, UrlStr);
            reqMe.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseMe = await CloudHttpHelper.SendAsync(reqMe);
            var resp =  await responseMe.Content.ReadAsStringAsync();
            return resp;
        }
        public static async Task<bool> RefreshTokenAsync()
        {
            string refreshStr = String.Format(ApiDictionary[DzApiEnum.LoginUrlString], service.ClientId, service.RedirectUri);
            Uri uri = new Uri(refreshStr);
            var req = new HttpRequestMessage(HttpMethod.Post, uri);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await CloudHttpHelper.SendAsync(req);
            var resp = await response.Content.ReadAsStringAsync();
            string[] newTok = DzParser.ParseFragment(req.RequestUri.Fragment);
            var connection = service.Connection as OAuth2ConnectionBase;
            
            if (newTok.Length == 2)
            {
                connection.Reset(newTok[0], newTok[1], "");
                (connection as ConnectBaseInterface).InvokeConnectionChange(connection as WebBasedConnectInterface);
                return true;
                
            }
            else
            {
                return false;
            }
        }

    }
}
