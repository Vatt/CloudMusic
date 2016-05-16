using System.Net.Http;
using CloudMusicLib.ServiceCore;
namespace CloudMusicLib.SoundCloudService
{
     public class SoundCloudService : CloudService
     {
        public string ClientId;
        public string SecretId;
        public string UserToken;
        /*
        static SoundCloudService()
        {
           CloudMan.RegisterService(new SoundCloudService());
        }
        */
         public SoundCloudService() : base("SoundCloud", false)
         {
            base.AddMethod(new ScAuthorizationMethod(this));
            base.AddMethod(new ScInitMethod(this));
         }    
    }
}
