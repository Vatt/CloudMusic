using System;
using System.Threading.Tasks;
using CloudMusicLib.ServiceCore;
using System.Diagnostics;
using CloudMusicLib.DeezerService.DeezerCore;
using CloudMusicLib.Common;

namespace CloudMusicLib.DeezerService
{
    public class DzConnection : OAuth2ConnectionBase, WebBasedConnectInterface
    {
        public ConnectionChangeEventHandler ConnectionChangeHandler { get; set; }
        public event ConnectionChangeEventHandler OnConnectionChanged;
        public void InvokeConnectionChange(ConnectBaseInterface connection)
        {
            OnConnectionChanged?.Invoke(connection);
        }
        public DzConnection(DeezerService service):base(service)
        {

        }
        string WebBasedConnectInterface.LoginUrlString
        {
            get
            {
                return DzApi.GetLoginUrlString();
            }  
        }

        bool ConnectBaseInterface.IsConnected()
        {
            if (_expiresIn == 0 || _accessToken.Length == 0)
            {
                return false;
            }
            return true;
        }


        async void WebBasedConnectInterface.Response(string response)
        {
            if (response.Length > 0)
            {
                string[] parsed = DzParser.ParseFragment(response);
                _accessToken = parsed[0];
                _expiresIn = Int32.Parse(parsed[1]);
                _refreshToken = "";
                var userJson = await DzApi.GetUserInfoJson(_accessToken);
                var user = DzParser.ParserUserInfo(userJson);
                _service.SetUser(user);
                _service.InvokeUserChange();
                InvokeConnectionChange(this);
            }
        }

        async void WebBasedConnectInterface.Response(Uri uri)
        {
            if (uri.Fragment.Length > 0)
            {
                string[] parsed = DzParser.ParseFragment(uri.Fragment);
                _accessToken = parsed[0];
                _expiresIn = Int32.Parse(parsed[1]);
                _refreshToken = "";
                var userJson = await DzApi.GetUserInfoJson(_accessToken);
                var user = DzParser.ParserUserInfo(userJson);
                _service.SetUser(user);
                _service.InvokeUserChange();
                InvokeConnectionChange(this);
            }
            

        }

        public override async Task<bool> RefreshAsync()
        {
            bool result = await DzApi.RefreshTokenAsync();
            if (!result)
            {
                return false;
            }
            return true;
        }
    }
}
