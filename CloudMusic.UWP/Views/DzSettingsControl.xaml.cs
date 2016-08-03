using CloudMusic.UWP.ViewModels.ServiceViewModels;
using CloudMusicLib.ServiceCore;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CloudMusic.UWP.Views
{
    public sealed partial class DzSettingsControl : UserControl
    {
        DzSettingsViewModel VM;
        public DzSettingsControl()
        {
            this.InitializeComponent();
            VM = new DzSettingsViewModel(CloudMan.Services().Single((service) => service.ServiceName.Equals("Deezer")));
            DeezerLoginWebView.Navigate(VM.LoginUrl);
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
