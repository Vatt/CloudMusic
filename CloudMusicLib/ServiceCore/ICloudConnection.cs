using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.ServiceCore
{
    public interface ICloudConnection
    {
        bool IsConnected();
        void Refresh();
        CloudService OwnerService();       
    }
}
