using CloudMusicLib.DeezerService.DeezerCore;
using CloudMusicLib.ServiceCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.DeezerService.DeezerMethods
{
    class DzTracksSearchMethod:ServiceMethod
    {
        public DzTracksSearchMethod(CloudService service) : base(service, ServiceCommands.SearchByTracks)
        {
        }

        public override ServiceResult<TOutType> Invoke<TOutType, TArgType>(params TArgType[] args)
        {
            var owner = DzApi.service;

            var a = (owner.Connection as OAuth2ConnectionBase).RefreshAsync().Result;

            string urlStr = DzApi.ApiDictionary[DzApiEnum.TracksSearch];
            string url = String.Format(urlStr, args[0],(owner.Connection as OAuth2ConnectionBase)._accessToken);
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = CloudHttpHelper.Send(req);
            string jsonData = response.Content.ReadAsStringAsync().Result;
            return null;
          //  var result = DzParser.ParseTrackListJson(JObject.Parse(jsonData));
          //  return result.ToServiceResult() as ServiceResult<TOutType>;
        }

        public override async Task<ServiceResult<TOutType>> InvokeAsync<TOutType, TArgType>(params TArgType[] args)
        {
            var owner = DzApi.service;

            await (owner.Connection as OAuth2ConnectionBase).RefreshAsync();

            string urlStr = DzApi.ApiDictionary[DzApiEnum.TracksSearch];
            string url = String.Format(urlStr, args[0], (owner.Connection as OAuth2ConnectionBase)._accessToken);
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await CloudHttpHelper.SendAsync(req);
            //var result = DzParser.ParseTrackListJson(
            //                        JObject.Parse(await response.Content.ReadAsStringAsync())
            //                      );
            //return result.ToServiceResult() as ServiceResult<TOutType>;
            return null;
        }
    }
}
