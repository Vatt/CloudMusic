using CloudMusicLib.Common;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System;

namespace CloudMusicLib.ServiceCore
{

    public abstract class CloudConnection
    {
        protected  ConnectionChangeEventHandler ConnectionChangeHandler;
        public event ConnectionChangeEventHandler OnConnectionChanged;
        protected void InvokeConnectionChange(CloudConnection connection)
        {
            OnConnectionChanged?.Invoke(connection);
        }
        protected CloudService _service;

        public abstract Task<bool> ConnectAsync<T>(params T[] args);
        public abstract void Disconnect();
        public abstract bool IsConnected();
        public CloudService OwnerService() => _service;
        public abstract string ToJsonString();
        public abstract void FromJsonString(string jsonString);
    }
    public abstract class OAuth2Connection : CloudConnection
    {
        public string _accessToken { get; protected set; }
        public string _refreshToken { get; protected set; }
        public int _expiresIn { get; protected set; }


        abstract override public Task<bool> ConnectAsync<T>(params T[] args);
        abstract override public bool IsConnected();
        abstract public Task<bool> RefreshAsync();

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

        public override void FromJsonString(string jsonString)
        {
            JObject json = JObject.Parse(jsonString);
            _accessToken = (string)json["access_token"];
            _refreshToken = (string)json["refresh_token"];
            _expiresIn = (int)json["expires_in"];

        }
        public override string ToJsonString()
        {
            JObject json = new JObject();
            json["access_token"] = _accessToken;
            json["refresh_token"] = _refreshToken;
            json["expires_in"] = _expiresIn;
            return json.ToString();
        }
        public override void Disconnect()
        {
            _accessToken = "";
            _refreshToken = "";
            _expiresIn = 0;
        }

    }
}
