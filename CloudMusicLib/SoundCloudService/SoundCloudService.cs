using CloudMusicLib.ServiceCore;
using CloudMusicLib.SoundCloudService.SoundCloudMethods;
namespace CloudMusicLib.SoundCloudService
{
     public class SoundCloudService : CloudService
     {
        public string ClientId;
        public string SecretId;
        public SoundCloudService() : base("SoundCloud", false)
         {
            base.Connection = new ScConnection(this);
            base.AddMethod(new ScAuthorizationMethod(this));
            base.AddMethod(new ScInitMethod(this));
            base.AddMethod(new ScGetUserPlaylistMethod(this));
            base.AddMethod(new ScTracksSearch(this));

         }
    }
}
