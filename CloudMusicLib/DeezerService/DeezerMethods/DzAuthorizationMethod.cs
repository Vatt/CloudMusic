using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudMusicLib.ServiceCore;
using CloudMusicLib.DeezerService.DeezerCore;

namespace CloudMusicLib.DeezerService.DeezerMethods
{
    /*Deezer авторизируется через браузер, на вход подается ответ из адресной строки:
     *redirect_uri#access_token=token_data&expires=number,
     * авторизация выполняется из вне
     */
    class DzAuthorizationMethod : ServiceMethod
    {
        public DzAuthorizationMethod(DeezerService service) : base(service, ServiceCommands.Authorization)
        {
        }

        public override ServiceResult<TOutType> Invoke<TOutType, TArgType>(params TArgType[] args)
        {
            var owner = DzApi.DzService;
            var ownerConnection = DzApi.GetServiceConnection();
            if (!ownerConnection.ConnectAsync(args[0], args[1]).Result)
            {
                return new ServiceResult<TOutType>(owner.ServiceName, ResultType.Err, "Авторизация не выполненна", null);
            }

            var result = new ServiceResult<TOutType>(owner.ServiceName, ResultType.Ok, owner.User as TOutType);
            return result;
        }

        public override async Task<ServiceResult<TOutType>> InvokeAsync<TOutType, TArgType>(params TArgType[] args)
        {
            var owner = DzApi.DzService;
            var ownerConnection = DzApi.GetServiceConnection();
            if (!await ownerConnection.ConnectAsync(args[0], args[1]))
            {
                return new ServiceResult<TOutType>(owner.ServiceName, ResultType.Err, "Авторизация не выполненна", null);
            }

            var result = new ServiceResult<TOutType>(owner.ServiceName, ResultType.Ok, owner.User as TOutType);
            return result;
        }
    }
}
