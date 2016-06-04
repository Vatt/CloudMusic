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
            string url = String.Format(urlStr, ownerCon.GetId(), owner.ClientId);
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = CloudHttpHelper.Send(req);
            var playlists = ScParser.ParsePlaylistsJson(JArray.Parse(response.Content.ReadAsStringAsync().Result));
            var result = new ScPlaylistsResult(ResultType.Ok, playlists);
            return result.ToServiceResult() as ServiceResult<TOutType>;
        }

        public override async  Task<ServiceResult<TOutType>> InvokeAsync<TOutType, TArgType>(params TArgType[] args)
        {
            var ownerCon = ScApi.GetServiceConnection();
            var owner = ScApi.ScService;
            if (!ownerCon.IsConnected()) { return new ServiceResult<TOutType>(owner.ServiceName, ResultType.Err, null); }           
            string urlStr = ScApi.ApiDictionary[ScApiEnum.MePlaylists];
            string url = String.Format(urlStr, ownerCon.GetId(),owner.ClientId);
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await CloudHttpHelper.SendAsync(req);
            var playlists = ScParser.ParsePlaylistsJson(JArray.Parse(await response.Content.ReadAsStringAsync()));
            var result = new ScPlaylistsResult(ResultType.Ok, playlists);
            return result.ToServiceResult() as ServiceResult<TOutType>;
        }
    }
}
