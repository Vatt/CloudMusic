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
    public class ScServiceTracksResult : ServiceResultCollection<CloudTrack>
    {
        public string _nextPageRef;
        public ScServiceTracksResult(ResultType type,List<CloudTrack> result,string nextPage) : base("SoundCloud", type,result)
        {
            base.IsIncrementalLoadingEnabled = true;
            _nextPageRef = nextPage;
        }
        public override bool HasMore() => _nextPageRef != null;
        public override List<CloudTrack> LoadNextIfPossible()
        {
            if (_nextPageRef==null) return new List<CloudTrack>();
            var req = new HttpRequestMessage(HttpMethod.Get, _nextPageRef);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = CloudHttpHelper.Send(req);
            var result = ScParser.ParseTrackListJson(
                                    JObject.Parse(response.Content.ReadAsStringAsync().Result)
                                  );
            _nextPageRef = result._nextPageRef;
            return result.Result;
        }
        public async  override Task<List<CloudTrack>> LoadNextIfPossibleAsync()
        {
            if (_nextPageRef == null) return new List<CloudTrack>();
            var req = new HttpRequestMessage(HttpMethod.Get, _nextPageRef);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await CloudHttpHelper.SendAsync(req);
            var result = ScParser.ParseTrackListJson(
                                    JObject.Parse(await response.Content.ReadAsStringAsync())
                                  );
            _nextPageRef = result._nextPageRef;
            return result.Result;
        }
    }
}
