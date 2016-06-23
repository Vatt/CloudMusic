using System;
using CloudMusicLib.ServiceCore;
using Newtonsoft.Json.Linq;
using CloudMusicLib.CoreLibrary;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using CloudMusicLib.Common;

namespace CloudMusicLib.SoundCloudService
{
    public class ScConnection:OAuth2Connection
    {
        public ScConnection(SoundCloudService service):base(service)
        {

        }

        public override async Task<bool> ConnectAsync<T>(params T[] args)
        {
            if (IsConnected())
            {
                return true;
            }
            string jsonStr = await ScApi.GetAuthDataJsonAsync(args[0], args[1]);
            JObject jsonAuth = JObject.Parse(jsonStr);
            JToken errorValue;
            if (jsonAuth.TryGetValue("error", out errorValue))
            {
                return false;
            }
            
            _accessToken = (string)jsonAuth["access_token"];
            _refreshToken = (string)jsonAuth["refresh_token"];
            _expiresIn = (int)jsonAuth["expires_in"];
      
            JObject jsonMe = JObject.Parse(ScApi.GetMeInfoJson(_accessToken));
            CloudUser user = new CloudUser(args[0] as string);
            user.UserName = (string)jsonMe["username"];
            if ((string)jsonMe["first_name"] != null)
            {
                user.FirstName = (string)jsonMe["first_name"];
            }
            if ((string)jsonMe["last_name"] != null)
            {
                user.LastName = (string)jsonMe["last_name"];
            }
            user.Id = (string)jsonMe["id"];
            OwnerService().SetUser(user);
            
 
            return true;
        }

        public override bool IsConnected()
        {
            if (_expiresIn == 0||_accessToken.Length==0||_refreshToken.Length==0)
            {
                return false;
            }
            return true;
        }

        public override async Task<bool> RefreshAsync()
        {
            var service = _service as SoundCloudService;
            var urlStr = ScApi.ApiDictionary[ScApiEnum.RefreshToken];
            var url = String.Format(urlStr, service.ClientId, service.SecretId, _refreshToken);
            var req = new HttpRequestMessage(HttpMethod.Post, url);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = CloudHttpHelper.Send(req);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return false;
            }
            var jsonStr = await response.Content.ReadAsStringAsync();
            JObject json;
            try
            {
                json = JObject.Parse(jsonStr);
            }catch(Exception e)
            {
                return false;
            }

            _accessToken = (string)json["access_token"];
            _refreshToken = (string)json["refresh_token"];
            _expiresIn = (int)json["expires_in"];

            base.InvokeConnectionChange(this);
            
            return false;
        }
    }
}
