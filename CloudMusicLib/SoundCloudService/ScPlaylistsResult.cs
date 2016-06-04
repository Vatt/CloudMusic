using CloudMusicLib.CoreLibrary;
using CloudMusicLib.ServiceCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            throw new NotImplementedException();
        }

        public override Task<List<CloudPlaylist>> LoadNextIfPossibleAsync()
        {
            throw new NotImplementedException();
        }
    }
}
