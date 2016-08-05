using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudMusicLib.ServiceCore;
using CloudMusicLib.DeezerService.DeezerMethods;
using CloudMusicLib.DeezerService.DeezerCore;
//http://connect.deezer.com/oauth/auth.php?app_id=184342&redirect_uri=https://connect.deezer.com/&response_type=token&perms=basic_access
namespace CloudMusicLib.DeezerService
{
    public class DeezerService : CloudService
    {
        public string ClientId;
        public string SecretId;
        public string RedirectUri;
        public DeezerService() : base("Deezer", true)
        {
            base.RegisterLoginMessage = "Регистрация обязательна";
            base.RegisterUri = new Uri("http://www.deezer.com/register");
            base.Connection = new DzConnection(this);

           // base.AddMethod(new DzAuthorizationMethod(this));
            base.AddMethod(new DzInitMethod(this));

            DzApi.Init(this);
        }
    }
}
