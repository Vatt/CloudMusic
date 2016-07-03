using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudMusicLib.ServiceCore;
namespace CloudMusicLib.DeezerService
{
    class DeezerService : CloudService
    {
        public DeezerService(string name) : base(name, true)
        {
        }
    }
}
