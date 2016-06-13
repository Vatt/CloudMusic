using System;
using CloudMusicLib.ServiceCore;
using Newtonsoft.Json.Linq;

namespace CloudMusicLib.SoundCloudService
{
    public class ScConnection:OAuth2Connection
    {
        public ScConnection(SoundCloudService service):base(service)
        {

        }

        public override bool Connect<T>(params T[] args)
        {
            string jsonStr = ScApi.GetAuthDataJsonAsync(args[0], args[1]).Result;
            JObject jsonAuth = JObject.Parse(jsonStr);
            JToken errorValue;
            if (jsonAuth.TryGetValue("error", out errorValue))
            {
                return false;
            }
            _accessToken = (string)jsonAuth["access_token"];
            _refreshToken = (string)jsonAuth["refresh_token"];
            _expiresIn = (int)jsonAuth["expires_in"];
            return true;
        }

        public override bool IsConnected()
        {
            throw new NotImplementedException();
        }

        public override bool Refresh()
        {
            throw new NotImplementedException();
        }
    }
}
