using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudMusicLib.ServiceCore;

namespace CloudMusicLib.SoundCloudService.SoundCloudMethods
{
    class ScInitMethod:ServiceMethod
    {
        public ScInitMethod(SoundCloudService service) : base(service, ServiceCommands.Init)
        {}

        public override TOutType Invoke<TOutType, TArgType>(params TArgType[] args)
        {
            var owner = base.Service as SoundCloudService;
            owner.ClientId = args[0].ToString();
            owner.SecretId = args[1].ToString();
            return default (TOutType);
        }
    }
}
