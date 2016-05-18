using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CloudMusicLib.ServiceCore;
using Newtonsoft.Json.Linq;

namespace CloudMusicLib.SoundCloudService.SoundCloudMethods
{
    class ScAuthorizationMethod:ServiceMethod
    {
        public ScAuthorizationMethod(SoundCloudService scService)
            : base(scService, ServiceCommands.Authorization)
        {}

        public override TOutType Invoke<TOutType, TArgType>( params TArgType[] args)
        {
            InvokeAsync<TOutType, TArgType>(args).Wait();
            return default(TOutType);
        }

        public override async Task<TOutType> InvokeAsync<TOutType, TArgType>(params TArgType[] args)
        {
            var owner = ScApi.ScService;
            var ownerConnection = ScApi.GetServiceConnection();
            string accessTok; string refreshTok;
            int    expiresIn; string idConn;
            if (ownerConnection == null)
            {
                ownerConnection = new ScConnection(owner);
            }
            JObject jsonAuth = JObject.Parse(await ScApi.GetAuthDataJson(args[0],args[1]));          
            accessTok = (string) jsonAuth["access_token"];
            refreshTok = (string) jsonAuth["refresh_token"];
            expiresIn = (int) jsonAuth["expires_in"];

            JObject jsonMe = JObject.Parse(await ScApi.GetMeInfoJson(accessTok));           
            idConn = (string) jsonMe["id"];
            ownerConnection.FillConnectionData(accessTok, refreshTok, expiresIn, idConn);
            return default(TOutType);
        }
    }
}
