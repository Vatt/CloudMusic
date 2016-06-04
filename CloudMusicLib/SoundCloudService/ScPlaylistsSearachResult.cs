using CloudMusicLib.CoreLibrary;
using CloudMusicLib.ServiceCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.SoundCloudService
{
    public class ScPlaylistsSearachResult : ServiceResultCollection<CloudPlaylist>
    {
        public ScPlaylistsSearachResult(ResultType type, List<CloudPlaylist> result) : base("SoundCloud", type, result)
        {
            base.IsIncrementalLoadingEnabled = true;
        }

        public override bool HasMore()
        {
            throw new NotImplementedException();
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
