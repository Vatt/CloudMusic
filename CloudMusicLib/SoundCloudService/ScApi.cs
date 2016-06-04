using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CloudMusicLib.ServiceCore;


namespace CloudMusicLib.SoundCloudService
{

    public class ScApi
    {
        public static readonly Dictionary<ScApiEnum, string> ApiDictionary = new Dictionary<ScApiEnum, string>
        {
            { ScApiEnum.Authorization, "https://api.soundcloud.com/oauth2/token?client_id={0}&client_secret={1}&grant_type=password&username={2}&password={3}"},
            { ScApiEnum.RefreshToken,  "https://api.soundcloud.com/oauth2/token&client_id={0}&client_secret={1}&grant_type=refresh_token&refresh_token={2}"},
            { ScApiEnum.Me,            "https://api.soundcloud.com/me?oauth_token={0}"},
            { ScApiEnum.MePlaylists,   "https://api.soundcloud.com/users/{0}/playlists?client_id={1}"},
            { ScApiEnum.MeTracks,      "https://api.soundcloud.com/users/{0}/tracks?client_id={1}"},
            { ScApiEnum.TracksSearch,  "https://api.soundcloud.com/tracks?q={0}&linked_partitioning=1&client_id={1}&limit=50" },

        };

        public static SoundCloudService ScService;

        static ScApi()
        {
           
        }

        public static void Init(SoundCloudService service) => ScService = service;

        public static ScConnection GetServiceConnection() => ScService.Connection as ScConnection;     
               
        public static async Task<string> GetAuthDataJsonAsync(object user, object password)
        {
            var authUrlStr = ApiDictionary[ScApiEnum.Authorization];
            var cliendId = ScService.ClientId;
            var secretId = ScService.SecretId;
            object[] formatArgs = { cliendId, secretId, user, password };
            string urlAuth = String.Format(authUrlStr, formatArgs);
            var reqAuth = new HttpRequestMessage(HttpMethod.Post, urlAuth);
            reqAuth.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseAuth = await CloudHttpHelper.SendAsync(reqAuth);
            return await responseAuth.Content.ReadAsStringAsync();
        }

        public static async Task<string> GetMeInfoJsonAsync(string accessTok)
        {
            var meUrlStr = ApiDictionary[ScApiEnum.Me];
            var meUrl = String.Format(meUrlStr, accessTok);
            var reqMe = new HttpRequestMessage(HttpMethod.Get, meUrl);
            reqMe.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseMe = await CloudHttpHelper.SendAsync(reqMe);
            return await responseMe.Content.ReadAsStringAsync();
        }
        public static string  GetAuthDataJson(object user, object password)
        {
            var authUrlStr = ApiDictionary[ScApiEnum.Authorization];
            var cliendId = ScService.ClientId;
            var secretId = ScService.SecretId;
            object[] formatArgs = { cliendId, secretId, user, password };
            string urlAuth = String.Format(authUrlStr, formatArgs);
            var reqAuth = new HttpRequestMessage(HttpMethod.Post, urlAuth);
            reqAuth.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseAuth = CloudHttpHelper.Send(reqAuth);
            return  responseAuth.Content.ReadAsStringAsync().Result;
        }

        public static string GetMeInfoJson(string accessTok)
        {
            var meUrlStr = ApiDictionary[ScApiEnum.Me];
            var meUrl = String.Format(meUrlStr, accessTok);
            var reqMe = new HttpRequestMessage(HttpMethod.Get, meUrl);
            reqMe.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseMe = CloudHttpHelper.Send(reqMe);
            return responseMe.Content.ReadAsStringAsync().Result;
        }
    }
}
