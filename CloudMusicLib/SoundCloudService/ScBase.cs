using System;
using System.Collections.Generic;


namespace CloudMusicLib.SoundCloudService
{

    class ScBase
    {
        public static readonly Dictionary<ScApiEnum, string> ApiDictionary = new Dictionary<ScApiEnum, string>
        {
            { ScApiEnum.Authorization, "https://api.soundcloud.com/oauth2/token?client_id={0}&client_secret={1}&grant_type=password&username={2}&password={3}"},
            { ScApiEnum.RefreshToken,  "https://api.soundcloud.com/oauth2/token&client_id={0}&client_secret={1}&grant_type=refresh_token&refresh_token={2}"},
            { ScApiEnum.MePlaylists,   ""},

        };
    }
}
