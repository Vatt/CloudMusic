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
    public class ScPlaylistsResult : ServiceResultCollection<CloudPlaylist>
    {
        private string _nextPageRef;
        public ScPlaylistsResult(ResultType type, List<CloudPlaylist> result, string nextPage) : base("SoundCloud", type, result)
        {
            base.IsIncrementalLoadingEnabled = true;
            _nextPageRef = nextPage;
        }

        public override bool HasMore()
        {
            return _nextPageRef != null;
        }

        public override List<CloudPlaylist> LoadNextIfPossible()
        {
            if (_nextPageRef == null) return new List<CloudPlaylist>();
            var req = new HttpRequestMessage(HttpMethod.Get, _nextPageRef);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = CloudHttpHelper.Send(req);
            var result = ScParser.ParsePlaylistsJson(
                                    JObject.Parse(response.Content.ReadAsStringAsync().Result)
                                  );
            _nextPageRef = result._nextPageRef;
            return result.Result;
        }

        public async override Task<List<CloudPlaylist>> LoadNextIfPossibleAsync()
        {
            if (_nextPageRef == null) return new List<CloudPlaylist>();
            var req = new HttpRequestMessage(HttpMethod.Get, _nextPageRef);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = CloudHttpHelper.Send(req);
            var result = ScParser.ParsePlaylistsJson(
                                     JObject.Parse(await response.Content.ReadAsStringAsync())
                                  );
            _nextPageRef = result._nextPageRef;
            return result.Result;
        }
    }
}
