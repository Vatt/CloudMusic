using System;
using CloudMusicLib.ServiceCore;

namespace CloudMusicLib.SoundCloudService
{
    public class ScConnection:ICloudConnection
    {
        private string _accessToken; 
        private string _refreshToken; 
        private string _id;
        private int    _expiresIn;
        private SoundCloudService _service;
        public ScConnection(SoundCloudService service)
        {
            _service = service;
        }
        public bool IsConnected()
        {
            return _accessToken != "" && _refreshToken != "";
        }
        public void Refresh()
        {
            throw new NotImplementedException();
        }
        
        public CloudService OwnerService()
        {
            return _service;
        }
        public void FillConnectionData(string accessTok, string refreshTok, int expiresIn, string id)
        {
            _accessToken = accessTok;
            _refreshToken = refreshTok;
            _expiresIn = expiresIn;
            _id = id;
        }

        public string GetId()
        {
            return _id;
        }
        public override string ToString()
        {
            string data = "SoundCloud Connection info: \n" +
                          "\tAccessToken: " + _accessToken + "\n" +
                          "\tRefreshToken: " + _refreshToken + "\n" +
                          "\tExpiresIn: " + _expiresIn + "\n" +
                          "\tUserId: " + _id + "\n";
            return data;
        }
    }
}
