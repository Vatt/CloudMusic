using CloudMusicLib.DeezerService;
using CloudMusicLib.ServiceCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace CloudMusic.UWP.ViewModels.ServiceViewModels
{
    class DzSettingsViewModel : ServiceSettingsViewModelBase
    {
        public bool IsConnected { get; set; }
        public bool IsUserCreated { get; set; }
        public Uri LoginUrl
        {
            get
            {
                return new Uri((_service.Connection as WebBasedConnectInterface).LoginUrlString);
            }
        }
        public DzSettingsViewModel(CloudService service) : base(service)
        {
            IsConnected = service.Connection.IsConnected();
            if(IsConnected)
            {
                (service.Connection as OAuth2ConnectionBase).RefreshAsync();
            }
            if (service.User!=null)
            {
                IsUserCreated = true;
                MessageIfAuthorized = $"Вы авторизованы:\n{_service.User.ToString()}";
            }
            else
            {
                IsUserCreated = false;
            }
        }
        public override void Logout()
        {
            throw new NotImplementedException();
        }

        public override void TryLogin()
        {
            var conn = (_service.Connection as WebBasedConnectInterface);
         //   conn.Response(webView.Source.AbsoluteUri);
        }
    }
}
