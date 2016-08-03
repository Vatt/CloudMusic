using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudMusicLib.ServiceCore;
using System.Diagnostics;

namespace CloudMusicLib.DeezerService
{
    public class DzConnection : OAuth2Connection, WebBasedLoginInterface
    {
        public string _jsCallback;
        public DeezerService service;
        public DzConnection(DeezerService service) : base(service)
        {
            this.service = service;
        }

        string[] WebBasedLoginInterface.GetJSCallbacks(string login, string password)
        {
            return new [] {
                        $"document.getElementById('login_mail').value = '{login}';",
                        $"document.getElementById('login_password').value ='{password}';",
                        $"document.getElementById('login_form_submit').click();"
            };
        }

        string WebBasedLoginInterface.LoginUrlString
        {
            get
            {
                return $"http://connect.deezer.com/oauth/auth.php?app_id={service.ClientId}&redirect_uri=https://connect.deezer.com/&response_type=token&perms=basic_access";
            }

  
        }

        public override Task<bool> ConnectAsync<T>(params T[] args)
        {
            throw new NotImplementedException();
        }

        public override bool IsConnected()
        {
            if (_expiresIn == 0 || _accessToken.Length == 0 || _refreshToken.Length == 0)
            {
                return false;
            }
            return true;
        }
        public override Task<bool> RefreshAsync()
        {
            throw new NotImplementedException();
        }

        public void Response(string response)
        {
            Debug.WriteLine(response);
        }
    }
}
