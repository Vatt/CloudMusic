using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudMusicLib.ServiceCore;
namespace CloudMusicLib.DeezerService.DeezerMethods
{
    class DzInitMethod:ServiceMethod
    {
        public DzInitMethod(CloudService service) : base(service, ServiceCommands.Init)
        {
        }

        public override ServiceResult<TOutType> Invoke<TOutType, TArgType>(params TArgType[] args)
        {
            var owner = base.Service as DeezerService;
            owner.ClientId = args[0].ToString();
            owner.SecretId = args[1].ToString();
            owner.RedirectUri = args[2].ToString();
            var result = new ServiceResult<TOutType>(owner.ServiceName, ResultType.Ok, default(TOutType));
            return result;
        }
    }
}
