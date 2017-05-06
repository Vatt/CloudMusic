using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudMusicLib.ServiceCore;
namespace CloudMusicLib.SpotifyService
{
    class SpotifyService : CloudService
    {
        public SpotifyService(string name, bool isAuthorizationRequired) : base(name, isAuthorizationRequired)
        {
        }
    }
}
