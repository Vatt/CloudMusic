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
        public ScPlaylistsResult(ResultType type, List<CloudPlaylist> result) : base("SoundCloud", type, result)
        {
            base.IsIncrementalLoadingEnabled = true;
        }
        public override List<CloudPlaylist> LoadNextIfPossible()
        {
            throw new NotImplementedException();
        }
    }
}
