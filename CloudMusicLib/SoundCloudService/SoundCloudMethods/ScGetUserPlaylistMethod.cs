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

        public override ServiceResult<TOutType> Invoke<TOutType, TArgType>(params TArgType[] args)
        {
            //return InvokeAsync<TOutType, TArgType>(args).Result;
            var ownerCon = ScApi.GetServiceConnection();
            var owner = ScApi.ScService;
            if (!ownerCon.IsConnected()) { return new ServiceResult<TOutType>(owner.ServiceName,ResultType.Err,null); }           
            string urlStr = ScApi.ApiDictionary[ScApiEnum.MePlaylists];
            string url = String.Format(urlStr, owner.User.Id, owner.ClientId);
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = CloudHttpHelper.Send(req);
            var jsonStr = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(jsonStr);
            JToken error;
            if (json.TryGetValue("error",out error))
            {
                if(ownerCon.RefreshAsync().Result)
                {
                    url = String.Format(urlStr, owner.User.Id, owner.ClientId);
                    req = new HttpRequestMessage(HttpMethod.Get, url);
                    req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = CloudHttpHelper.Send(req);
                    jsonStr = response.Content.ReadAsStringAsync().Result;
                    json = JObject.Parse(jsonStr);
                    if (json.TryGetValue("error", out error)) { return new ServiceResult<TOutType>(owner.ServiceName, ResultType.Err, null); }
                }
            }
            var playlists = ScParser.ParsePlaylistsJsonDirect(json);
            return playlists.ToServiceResult() as ServiceResult<TOutType>;
        }

        public override async  Task<ServiceResult<TOutType>> InvokeAsync<TOutType, TArgType>(params TArgType[] args)
        {
            var ownerCon = ScApi.GetServiceConnection();
            var owner = ScApi.ScService;
            if (!ownerCon.IsConnected()) { return new ServiceResult<TOutType>(owner.ServiceName, ResultType.Err, null); }           
            string urlStr = ScApi.ApiDictionary[ScApiEnum.MePlaylists];
            string url = String.Format(urlStr, owner.User.Id, owner.ClientId);
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await CloudHttpHelper.SendAsync(req);
            var jsonStr = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(jsonStr);
            JToken error;
            if (json.TryGetValue("error", out error))
            {
                if (await ownerCon.RefreshAsync())
                {
                    url = String.Format(urlStr, owner.User.Id, owner.ClientId);
                    req = new HttpRequestMessage(HttpMethod.Get, url);
                    req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = CloudHttpHelper.Send(req);
                    jsonStr = await response.Content.ReadAsStringAsync();
                    json = JObject.Parse(jsonStr);
                    if (json.TryGetValue("error", out error)) { return new ServiceResult<TOutType>(owner.ServiceName, ResultType.Err, null); }
                }
            }
            var playlists = ScParser.ParsePlaylistsJsonDirect(json);
            return playlists.ToServiceResult() as ServiceResult<TOutType>;
        }
    }
}
