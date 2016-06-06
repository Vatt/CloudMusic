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
            ScPlaylistsResult result = null;
            var jsonStr = response.Content.ReadAsStringAsync().Result;
            try
            {
                var json = JObject.Parse(jsonStr);
                result = ScParser.ParsePlaylistsJson(json);
            }
            catch (Exception) { }
            

            if (result==null) return new List<CloudPlaylist>();
            _nextPageRef = result._nextPageRef;
            return result.Result;
        }

        public async override Task<List<CloudPlaylist>> LoadNextIfPossibleAsync()
        {
            if (_nextPageRef == null) return new List<CloudPlaylist>();
            var req = new HttpRequestMessage(HttpMethod.Get, _nextPageRef);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await CloudHttpHelper.SendAsync(req);
            var jsonStr = await response.Content.ReadAsStringAsync();
            ScPlaylistsResult result = null;
            try
            {
                var json = JObject.Parse(jsonStr);
                result = ScParser.ParsePlaylistsJson(json);
            }
            catch (Exception) { }
            if (result == null) return new List<CloudPlaylist>();
            _nextPageRef = result._nextPageRef;
            return result.Result;
        }
    }
}
