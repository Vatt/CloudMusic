using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudMusicLib.ServiceCore;
namespace CloudMusicLib.SoundCloudService
{
     public class SoundCloudService : ICloudService
    {
        public bool IsAuthorizationRequired    { get { return false; } }

        public string ServiceName           { get { return "SoundCloud"; } }

        public bool Authorization(string user, string password)
        {
            throw new NotImplementedException();
        }
    }
}
