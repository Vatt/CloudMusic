using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CloudMusicLib.CoreLibrary;
using CloudMusicLib.ServiceCore;
using Newtonsoft.Json.Linq;

namespace CloudMusicLib.SoundCloudService.SoundCloudMethods
{
    class ScTracksSearch:ServiceMethod
    {
        public ScTracksSearch(CloudService service) : base(service, ServiceCommands.SearchByTracks)
        {
        }

        public override TOutType Invoke<TOutType, TArgType>(params TArgType[] args)
        {
            return InvokeAsync<TOutType, TArgType>(args).Result;
        }

        public override async Task<TOutType> InvokeAsync<TOutType, TArgType>(params TArgType[] args)
        {
            var owner = ScApi.ScService;
            string urlStr = ScApi.ApiDictionary[ScApiEnum.TracksSearch];
            string url = String.Format(urlStr, args[0], owner.ClientId);
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await CloudHttpHelper.SendAsync(req);
            CloudTracklist data = ScParser.ParseTackListJson(
                                    JArray.Parse(await response.Content.ReadAsStringAsync())
                                  );
            return data as TOutType;
        }
    }
}
