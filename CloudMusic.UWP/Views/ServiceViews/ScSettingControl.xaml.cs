using CloudMusic.UWP.ViewModels.ServiceViewModels;
using CloudMusicLib.ServiceCore;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CloudMusic.UWP.Views.ServiceViews
{
    public sealed partial class ScSettingControl : UserControl
    {
        ScSettingBaseViewModel VM { get; set; }
        public ScSettingControl()
        {       
            this.InitializeComponent();
            VM = new ScSettingBaseViewModel(CloudMan.Services().Single((service) => service.ServiceName.Equals("SoundCloud")));         
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
