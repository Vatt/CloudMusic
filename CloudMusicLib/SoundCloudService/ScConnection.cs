using System;
using CloudMusicLib.ServiceCore;

namespace CloudMusicLib.SoundCloudService
{
    public class ScConnection:OAuth2Connection
    {
        public ScConnection(SoundCloudService service):base(service)
        {

        }

        public override bool IsConnected()
        {
            throw new NotImplementedException();
        }

        public override void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}
