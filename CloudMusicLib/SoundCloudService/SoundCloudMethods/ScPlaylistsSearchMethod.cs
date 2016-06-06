using CloudMusicLib.ServiceCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.SoundCloudService.SoundCloudMethods
{
    class ScPlaylistsSearchMethod : ServiceMethod
    {
        public ScPlaylistsSearchMethod(CloudService service) : base(service, ServiceCommands.SearchByPlaylists)
        {
        }

        public override ServiceResult<TOutType> Invoke<TOutType, TArgType>(params TArgType[] args)
        {
            var owner = ScApi.ScService;
            string urlStr = ScApi.ApiDictionary[ScApiEnum.PlaylistsSearch];
            string url = String.Format(urlStr, args[0], owner.ClientId);
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = CloudHttpHelper.Send(req);
            string jsonData = response.Content.ReadAsStringAsync().Result;

            var result = ScParser.ParsePlaylistsJson(JObject.Parse(jsonData));
            return result.ToServiceResult() as ServiceResult<TOutType>;
        }
        public override async Task<ServiceResult<TOutType>> InvokeAsync<TOutType, TArgType>(params TArgType[] args)
        {
            var owner = ScApi.ScService;
            string urlStr = ScApi.ApiDictionary[ScApiEnum.PlaylistsSearch];
            string url = String.Format(urlStr, args[0], owner.ClientId);
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await CloudHttpHelper.SendAsync(req);
            string jsonData = await response.Content.ReadAsStringAsync();

            var result = ScParser.ParsePlaylistsJson(JObject.Parse(jsonData));
            return result.ToServiceResult() as ServiceResult<TOutType>;
        }
    }
}
