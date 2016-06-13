using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace CloudMusicLib.ServiceCore
{
    public interface ICloudConnection
    {
        Task<bool> ConnectAsync<T>(params T[] args);
        bool IsConnected();
        CloudService OwnerService();
        string ToJsonString();
        void FromJsonString(string jsonString);
    }
    public abstract class OAuth2Connection : ICloudConnection
    {
        public string _accessToken { get; protected set; }
        public string _refreshToken { get; protected set; }
        public int _expiresIn { get; protected set; }
        private CloudService _service;
        public CloudService OwnerService() =>_service;

        abstract public Task<bool> ConnectAsync<T>(params T[] args);
        abstract public bool IsConnected();
        abstract public bool Refresh();

        public OAuth2Connection(CloudService service)
        {
            _service = service;
            _accessToken = "";
            _refreshToken = "";
            _expiresIn = 0;
        }


        public override string ToString()
        {
            string data = "OAuth2Connection info: \n" +
                          "\tAccessToken: " + _accessToken + "\n" +
                          "\tRefreshToken: " + _refreshToken + "\n" +
                          "\tExpiresIn: " + _expiresIn + "\n";
            return data;
        }

        public virtual void FromJsonString(string jsonString)
        {
            JObject json = JObject.Parse(jsonString);
            _accessToken = (string)json["access_token"];
            _refreshToken = (string)json["refresh_token"];
            _expiresIn = (int)json["expires_in"];

        }
        public virtual string ToJsonString()
        {
            JObject json = new JObject();
            json["access_token"] = _accessToken;
            json["refresh_token"] = _refreshToken;
            json["expires_in"] = _expiresIn;
            return json.ToString();
        }

    }
}
