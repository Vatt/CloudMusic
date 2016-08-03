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
        public Uri LoginUrl
        {
            get
            {
                return new Uri((_service.Connection as WebBasedLoginInterface).LoginUrlString);
            }
        }
        public DzSettingsViewModel(CloudService service) : base(service)
        { }
        public override void Logout()
        {
            throw new NotImplementedException();
        }

        public override void TryLogin()
        {
            /*
            webView.DOMContentLoaded += async (w, args) =>
            {
                await w.InvokeScriptAsync("eval", (_service.Connection as WebBasedLoginInterface).GetJSCallbacks(Login, Password));
            };
            webView.NavigationCompleted += (s, a) =>
            {
                var conn = (_service.Connection as WebBasedLoginInterface);
                conn.Response(webView.Source.AbsoluteUri);
            };
            */
        }
    }
}
