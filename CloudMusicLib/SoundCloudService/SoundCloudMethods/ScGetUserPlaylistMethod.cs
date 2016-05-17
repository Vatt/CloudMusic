using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CloudMusicLib.ServiceCore;

namespace CloudMusicLib.SoundCloudService.SoundCloudMethods
{
    class ScGetUserPlaylistMethod:ServiceMethod
    {
        public ScGetUserPlaylistMethod(SoundCloudService service) : base(service, ServiceCommands.GetUserPlaylists)
        {
        }

        public override TOutType Invoke<TOutType, TArgType>(params TArgType[] args)
        {
            return InvokeAsync<TOutType, TArgType>(args).Result;
        }

        public override async  Task<TOutType> InvokeAsync<TOutType, TArgType>(params TArgType[] args)
        {
            var ownerCon = Service.Connection as ScConnection;
            var owner = Service as SoundCloudService;
            string urlStr = ScBase.ApiDictionary[ScApiEnum.MePlaylists];
            string url = String.Format(urlStr, ownerCon.GetId(),owner.ClientId);
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await CloudHttpHelper.SendAsync(req);
            return await response.Content.ReadAsStringAsync() as TOutType;
        }
    }
}
