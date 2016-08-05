using CloudMusic.UWP.ViewModels.ServiceViewModels;
using CloudMusicLib.ServiceCore;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CloudMusic.UWP.Views.ServiceViews
{
    public sealed partial class DzSettingsControl : UserControl
    {
        DzSettingsViewModel VM;
        public DzSettingsControl()
        {
            VM = new DzSettingsViewModel(CloudMan.Services().Single((service) => service.ServiceName.Equals("Deezer")));
            this.InitializeComponent();
            if (!VM.IsConnected)
            {
                DeezerLoginWebView.Navigate(VM.LoginUrl);
            }
            
            DeezerLoginWebView.NavigationCompleted += (a, b) =>
            {
                (VM._service.Connection as WebBasedConnectInterface).Response(a.Source);
            };
           
        }
        private void Grid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                VM.TryLogin();
            }
        }
    }
}
