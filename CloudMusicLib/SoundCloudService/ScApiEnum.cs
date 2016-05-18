using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.SoundCloudService
{
    public enum ScApiEnum
    {
        Authorization,//  /oauth2/token
        RefreshToken,

        Me,
        MePlaylists,
        MeTracks,

        TracksSearch,
    }
}
