using Newtonsoft.Json.Linq;
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
        CloudService OwnerService();
        string ToJsonString();  
    }
    public abstract class OAuth2Connection : ICloudConnection
    {
        private string _accessToken;
        private string _refreshToken;
        private int _expiresIn;
        private CloudService _service;
        public CloudService OwnerService() =>_service;
        public OAuth2Connection(CloudService service)
        {
            _service = service;
        }
        public void FillConnectionData(string accessTok, string refreshTok, int expiresIn)
        {
            _accessToken = accessTok;
            _refreshToken = refreshTok;
            _expiresIn = expiresIn;
        }
        public override string ToString()
        {
            string data = "OAuth2Connection info: \n" +
                          "\tAccessToken: " + _accessToken + "\n" +
                          "\tRefreshToken: " + _refreshToken + "\n" +
                          "\tExpiresIn: " + _expiresIn + "\n";
            return data;
        }
        abstract public bool IsConnected();        
        abstract public void Refresh();
        public void FromJsonString(string jsonString)
        {
            JObject json = JObject.Parse(jsonString);
            _accessToken = (string)json["access_token"];
            _refreshToken = (string)json["refresh_token"];
            _expiresIn = (int)json["expires_in"];

        }
        public string ToJsonString()
        {
            JObject json = new JObject();
            json["access_token"] = _accessToken;
            json["refresh_token"] = _refreshToken;
            json["expires_in"] = _expiresIn;
            return json.ToString();
        }
    }
}
