using Newtonsoft.Json.Linq;


namespace CloudMusicLib.ServiceCore
{
    public interface ICloudConnection
    {
        bool Connect<T>(params T[] args);
        bool IsConnected();
        CloudService OwnerService();
        string ToJsonString();
        void FromJsonString(string jsonString);
    }
    public abstract class OAuth2Connection : ICloudConnection
    {
        protected string _accessToken;
        protected string _refreshToken;
        protected int _expiresIn;
        private CloudService _service;
        public CloudService OwnerService() =>_service;

        abstract public bool Connect<T>(params T[] args);
        abstract public bool IsConnected();
        abstract public bool Refresh();

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

        public  void FromJsonString(string jsonString)
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
