using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CloudMusicLib.ServiceCore;
using Newtonsoft.Json.Linq;
using CloudMusicLib.CoreLibrary;

namespace CloudMusicLib.SoundCloudService.SoundCloudMethods
{
    class ScAuthorizationMethod:ServiceMethod
    {
        public ScAuthorizationMethod(SoundCloudService scService)
            : base(scService, ServiceCommands.Authorization)
        {}

        public override ServiceResult<TOutType> Invoke<TOutType, TArgType>( params TArgType[] args)
        {
            var owner = ScApi.ScService;
            var ownerConnection = ScApi.GetServiceConnection();
            string accessTok; string refreshTok;
            int expiresIn; string idConn;
            if (ownerConnection == null)
            {
                ownerConnection = new ScConnection(owner);
            }
            string jsonStr = ScApi.GetAuthDataJsonAsync(args[0], args[1]).Result;
            JObject jsonAuth = JObject.Parse(jsonStr);
            JToken errorValue;
            if (jsonAuth.TryGetValue("error",out errorValue))
            {
                return new ServiceResult<TOutType>(owner.ServiceName, ResultType.Err,$"Error: {errorValue.ToString()}", default(TOutType));
            }
            accessTok = (string)jsonAuth["access_token"];
            refreshTok = (string)jsonAuth["refresh_token"];
            expiresIn = (int)jsonAuth["expires_in"];            
            JObject jsonMe = JObject.Parse(ScApi.GetMeInfoJson(accessTok));
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
            owner.SetUser(user);
            idConn = (string)jsonMe["id"];
            ownerConnection.FillConnectionData(accessTok, refreshTok, expiresIn, idConn);
            var result = new ServiceResult<TOutType>(owner.ServiceName, ResultType.Ok, default(TOutType));
            return result;
        }

        public override async Task<ServiceResult<TOutType>> InvokeAsync<TOutType, TArgType>(params TArgType[] args)
        {
            var owner = ScApi.ScService;
            var ownerConnection = ScApi.GetServiceConnection();
            string accessTok; string refreshTok;
            int    expiresIn; string idConn;
            if (ownerConnection == null)
            {
                ownerConnection = new ScConnection(owner);
            }
            string jsonStr = await ScApi.GetAuthDataJsonAsync(args[0], args[1]);
            JObject jsonAuth = JObject.Parse(jsonStr);
            JToken errorValue;
            if (jsonAuth.TryGetValue("error", out errorValue))
            {
                return new ServiceResult<TOutType>(owner.ServiceName, ResultType.Err, $"Error: {errorValue.ToString()}", default(TOutType));
            }
            accessTok = (string) jsonAuth["access_token"];
            refreshTok = (string) jsonAuth["refresh_token"];
            expiresIn = (int) jsonAuth["expires_in"];

            JObject jsonMe = JObject.Parse(await ScApi.GetMeInfoJsonAsync(accessTok));
            CloudUser user = new CloudUser(args[0] as string);
            user.Login = args[0] as string;
            user.UserName = (string)jsonMe["username"];
            if ((string)jsonMe["first_name"] != null)
            {
                user.FirstName = (string)jsonMe["first_name"];
            }
            if ((string)jsonMe["last_name"] != null)
            {
                user.LastName = (string)jsonMe["last_name"];
            }
            owner.SetUser(user);
            idConn = (string) jsonMe["id"];
            ownerConnection.FillConnectionData(accessTok, refreshTok, expiresIn, idConn);
            var result = new ServiceResult<TOutType>(owner.ServiceName, ResultType.Ok, default(TOutType));
            return result;
        }
    }
}
