using CloudMusicLib.CoreLibrary;
using CloudMusicLib.ServiceCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.SoundCloudService
{
    class ScLazyTracklist : LazyLoad<CloudTracklist>
    {
        private Uri _refToTracklist;
        public ScLazyTracklist(string uriStr)
        {
            _refToTracklist = new Uri(uriStr);
        }
        protected override async Task<CloudTracklist> CreateAsync()
        {
            CloudTracklist tracklist = new CloudTracklist(CloudListMode.Constant);
            var req = new HttpRequestMessage(HttpMethod.Get, _refToTracklist);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await CloudHttpHelper.SendAsync(req);
            var result = ScParser.ParseTrackListJson(
                                    JObject.Parse(await response.Content.ReadAsStringAsync())
                                  );
            if (result.Type == ResultType.Ok)
            {
                tracklist.MergeOther(ScApi.ScService.ServiceName, result.Result);
                return tracklist;
            }
            else
            {
                throw new Exception("ScLazyTracklist returned Error");
            }
        }
    }
}
