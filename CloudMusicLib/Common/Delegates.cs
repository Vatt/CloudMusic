

using CloudMusicLib.ServiceCore;

namespace CloudMusicLib.Common
{
    public delegate void ConnectionChangeEventHandler(CloudConnection connection);
    /*callback для логина на основе веб вьюх(например Deezer) после логина должен прийти ответ*/
    public delegate void WebBaseConnectionCallback(string response);
}
