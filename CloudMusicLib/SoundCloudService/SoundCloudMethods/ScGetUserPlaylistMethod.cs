using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CloudMusicLib.ServiceCore;
using Newtonsoft.Json.Linq;

namespace CloudMusicLib.SoundCloudService.SoundCloudMethods
{
    class ScGetUserPlaylistMethod:ServiceMethod
    {
        public ScGetUserPlaylistMethod(SoundCloudService service) : base(service, ServiceCommands.GetUserPlaylists)
        {
        }

        public override TOutType Invoke<TOutType, TArgType>(params TArgType[] args)
        {
            //return InvokeAsync<TOutType, TArgType>(args).Result;
            var ownerCon = ScApi.GetServiceConnection();
            if (!ownerCon.IsConnected()) { return default(TOutType); }
            var owner = ScApi.ScService;
            string urlStr = ScApi.ApiDictionary[ScApiEnum.MePlaylists];
            string url = String.Format(urlStr, ownerCon.GetId(), owner.ClientId);
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = CloudHttpHelper.Send(req);
            var playlists = ScParser.ParsePlaylistsJson(JArray.Parse(response.Content.ReadAsStringAsync().Result));
            return playlists as TOutType;
        }

        public override async  Task<TOutType> InvokeAsync<TOutType, TArgType>(params TArgType[] args)
        {
            var ownerCon = ScApi.GetServiceConnection();
            if (!ownerCon.IsConnected()) {  return default(TOutType);}
            var owner = ScApi.ScService;
            string urlStr = ScApi.ApiDictionary[ScApiEnum.MePlaylists];
            string url = String.Format(urlStr, ownerCon.GetId(),owner.ClientId);
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await CloudHttpHelper.SendAsync(req);
            var playlists = ScParser.ParsePlaylistsJson(JArray.Parse(await response.Content.ReadAsStringAsync()));
            return playlists as TOutType;
        }
    }
}
