using CloudMusicLib.Common;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System;

namespace CloudMusicLib.ServiceCore
{
    public interface ConnectBaseInterface
    {
        ConnectionChangeEventHandler ConnectionChangeHandler   { get; set; }
        event ConnectionChangeEventHandler OnConnectionChanged;
        void InvokeConnectionChange(ConnectBaseInterface connection);
        bool IsConnected();
        string ToJsonString();
        void FromJsonString(string jsonString);
    }
    public interface ConnectInterface: ConnectBaseInterface
    {
        Task<bool> ConnectAsync<T>(params T[] args);
        void Disconnect();
    }

    public abstract class CloudConnection: ConnectInterface
    {
        /*protected  ConnectionChangeEventHandler ConnectionChangeHandler;
        public event ConnectionChangeEventHandler OnConnectionChanged;*/
        public void InvokeConnectionChange(CloudConnection connection)
        {
            OnConnectionChanged?.Invoke(connection);
        }
        protected CloudService _service;

        public event ConnectionChangeEventHandler OnConnectionChanged;

        public ConnectionChangeEventHandler ConnectionChangeHandler { get; set; }
        

        public abstract Task<bool> ConnectAsync<T>(params T[] args);
        public abstract void Disconnect();
        public abstract bool IsConnected();
        public CloudService OwnerService() => _service;
        public abstract string ToJsonString();
        public abstract void FromJsonString(string jsonString);

        public bool isConnected()
        {
            throw new NotImplementedException();
        }

        public abstract void InvokeConnectionChange(ConnectBaseInterface connection);
    }
    public abstract class OAuth2ConnectionBase
    {
        protected CloudService _service;
        public string _accessToken { get; protected set; }
        public string _refreshToken { get; protected set; }
        public int _expiresIn { get; protected set; }
        public OAuth2ConnectionBase(CloudService service)
        {
            _service = service;
            _accessToken = "";
            _refreshToken = "";
            _expiresIn = 0;
        }
        public void Reset(string AccessTok, string Expires, string RefreshTok)
        {
            _accessToken = AccessTok;
            _expiresIn = Int32.Parse(Expires);
            _refreshToken = RefreshTok;
        }

        public override string ToString()
        {
            string data = "Connection info: \n" +
                          "\tAccessToken: " + _accessToken + "\n" +
                          "\tRefreshToken: " + _refreshToken + "\n" +
                          "\tExpiresIn: " + _expiresIn + "\n";
            return data;
        }

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
        public void Disconnect()
        {
            _accessToken = "";
            _refreshToken = "";
            _expiresIn = 0;
        }
        abstract public Task<bool> RefreshAsync();
    }
    public abstract class OAuth2Connection : OAuth2ConnectionBase, ConnectInterface
    {
        public ConnectionChangeEventHandler ConnectionChangeHandler { get; set; }

        public event ConnectionChangeEventHandler OnConnectionChanged;
        public void InvokeConnectionChange(ConnectBaseInterface connection)
        {
            OnConnectionChanged?.Invoke(connection);
        }
        abstract public Task<bool> ConnectAsync<T>(params T[] args);
        abstract public bool IsConnected();
        abstract override public Task<bool> RefreshAsync();

        public OAuth2Connection(CloudService service):base(service)
        {
            _service = service;
            _accessToken = "";
            _refreshToken = "";
            _expiresIn = 0;
        }
    }
    public interface WebBasedConnectInterface: ConnectBaseInterface
    {
        string LoginUrlString { get; }
        void Response(string response);
        void Response(Uri uri);
        
    }

}
