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
    public class ScLazyTracklist : LazyLoad<CloudTracklist>
    {
        private Uri _refToTracklist;
        public ScLazyTracklist(string uriStr)
        {
            _refToTracklist = new Uri(uriStr);
        }
        protected override async Task<CloudTracklist> CreateAsync()
        {
            CloudTracklist tracklist = new CloudTracklist(CloudListMode.Dynamic);
            var req = new HttpRequestMessage(HttpMethod.Get, _refToTracklist);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = CloudHttpHelper.Send(req);
            var responseData = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(responseData);
            var result = ScParser.ParseTrackListJson(json);
            if (result.Type == ResultType.Ok)
            {
                tracklist.MergeOther(result);
                return tracklist;
            }
            else
            {
                throw new Exception("ScLazyTracklist returned Error");
            }
        }
        protected override CloudTracklist Create()
        {
            CloudTracklist tracklist = new CloudTracklist(CloudListMode.Constant);
            var req = new HttpRequestMessage(HttpMethod.Get, _refToTracklist);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ScTracksResult result = null; 
            var response = CloudHttpHelper.Send(req);
            try
            {
                result = ScParser.ParseTrackListJson(
                                        JObject.Parse(response.Content.ReadAsStringAsync().Result)
                                      );
            }catch(Exception e)
            {
                result = new ScTracksResult(ResultType.Err, null, null);
            }
            if (result.Type == ResultType.Ok)
            {
                tracklist.MergeOther(ScApi.ScService.ServiceName, result.Result);
                return tracklist;
            }
            else
            {
                return tracklist;
                //throw new Exception("ScLazyTracklist returned Error");
            }
        }
    }
}
