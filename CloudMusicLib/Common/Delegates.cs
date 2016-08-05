

using CloudMusicLib.CoreLibrary;
using CloudMusicLib.ServiceCore;

namespace CloudMusicLib.Common
{
    public delegate void ConnectionChangeEventHandler(ConnectBaseInterface connection);
    public delegate void UserChangeEventHandler(CloudService service);
}
