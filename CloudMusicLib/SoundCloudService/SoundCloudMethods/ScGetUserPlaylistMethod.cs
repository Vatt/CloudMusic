using System;
using System.Threading.Tasks;
using CloudMusicLib.ServiceCore;

namespace CloudMusicLib.SoundCloudService.SoundCloudMethods
{
    class ScGetUserPlaylistMethod:ServiceMethod
    {
        public ScGetUserPlaylistMethod(SoundCloudService service) : base(service, ServiceCommands.GetUserPlaylists)
        {
        }

        public override TOutType Invoke<TOutType, TArgType>(params TArgType[] args)
        {
            throw new NotImplementedException();
        }

        public override Task<TOutType> InvokeAsync<TOutType, TArgType>(params TArgType[] args)
        {

            return default(Task<TOutType>);
        }
    }
}
