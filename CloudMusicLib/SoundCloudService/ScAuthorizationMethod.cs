using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CloudMusicLib.ServiceCore;

namespace CloudMusicLib.SoundCloudService
{
    class ScAuthorizationMethod:ServiceMethod
    {
        public ScAuthorizationMethod(SoundCloudService scService)
            : base(scService, ServiceCommands.Authorization)
        {}

        public override TOutType Invoke<TOutType, TArgType>( params TArgType[] args)
        {
            var owner = base.Service as SoundCloudService;
            var uriStr = ScBase.ApiDictionary[ScApiEnum.Authorization];
            var cliendId = owner.ClientId;
            var secretId = owner.SecretId;
            object[] formatArgs = { cliendId, secretId, args[0], args[1] };
            string url = String.Format(uriStr, formatArgs);
            var req = new HttpRequestMessage(HttpMethod.Post, url);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = CloudHttpHelper.SendAsync(req).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return data as TOutType;
        }

        public override async Task<TOutType> InvokeAsync<TOutType, TArgType>(params TArgType[] args)
        {
            var owner = base.Service as SoundCloudService;
            var uriStr = ScBase.ApiDictionary[ScApiEnum.Authorization];
            var cliendId = owner.ClientId;
            var secretId = owner.SecretId;
            object[] formatArgs = { cliendId, secretId, args[0], args[1] };
            string url = String.Format(uriStr, formatArgs);
            var req = new HttpRequestMessage(HttpMethod.Post, url);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await CloudHttpHelper.SendAsync(req);
            return response.Content.ReadAsStringAsync() as TOutType;

        }
    }
}
