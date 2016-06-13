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
            if(!ownerConnection.ConnectAsync(args[0], args[1]).Result)
            {
                return new ServiceResult<TOutType>(owner.ServiceName, ResultType.Err, "Авторизация не выполненна", null);
            }       

            var result = new ServiceResult<TOutType>(owner.ServiceName, ResultType.Ok, owner.User as TOutType);
            return result;
        }

        public override async Task<ServiceResult<TOutType>> InvokeAsync<TOutType, TArgType>(params TArgType[] args)
        {
            var owner = ScApi.ScService;
            var ownerConnection = ScApi.GetServiceConnection();
            if (! await ownerConnection.ConnectAsync(args[0], args[1]))
            {
                return new ServiceResult<TOutType>(owner.ServiceName, ResultType.Err, "Авторизация не выполненна", null);
            }

            var result = new ServiceResult<TOutType>(owner.ServiceName, ResultType.Ok, owner.User as TOutType);
            return result;
        }
    }
}
