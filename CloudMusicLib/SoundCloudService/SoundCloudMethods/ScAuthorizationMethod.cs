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
            var owner = base.Service as SoundCloudService;
            var ownerConnection = base.Service.Connection as ScConnection;
            var authUrlStr = ScBase.ApiDictionary[ScApiEnum.Authorization];
            var cliendId = owner.ClientId;
            var secretId = owner.SecretId;
            string accessTok; string refreshTok;
            int    expiresIn; string idConn;
            if (ownerConnection == null)
            {
                ownerConnection = new ScConnection(owner);
            }
            object[] formatArgs = { cliendId, secretId, args[0], args[1] };
            string urlAuth = String.Format(authUrlStr, formatArgs);
            var reqAuth = new HttpRequestMessage(HttpMethod.Post, urlAuth);
            reqAuth.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseAuth = await CloudHttpHelper.SendAsync(reqAuth);
            JObject jsonAuth = JObject.Parse(await responseAuth.Content.ReadAsStringAsync());
           
            accessTok = (string) jsonAuth["access_token"];
            refreshTok = (string) jsonAuth["refresh_token"];
            expiresIn = (int) jsonAuth["expires_in"];

            var meUrlStr = ScBase.ApiDictionary[ScApiEnum.Me];
            var meUrl = String.Format(meUrlStr, accessTok);
            var reqMe = new HttpRequestMessage(HttpMethod.Get, meUrl);
            reqMe.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseMe = await CloudHttpHelper.SendAsync(reqMe);
            JObject jsonMe = JObject.Parse(await responseMe.Content.ReadAsStringAsync());

            idConn = (string) jsonMe["id"];

            ownerConnection.FillConnectionData(accessTok, refreshTok, expiresIn, idConn);
            return default(TOutType);
        }
    }
}
