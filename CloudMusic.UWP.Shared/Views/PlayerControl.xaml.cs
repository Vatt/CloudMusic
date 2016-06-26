using CloudMusic.UWP.Common;
using CloudMusic.UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CloudMusic.UWP.Views
{
    public sealed partial class PlayerControl : UserControl
    {
        private PlayerControlViewModel _vm;

        public PlayerControl()
        {
            _vm = AppStaticData.Player;
            this.InitializeComponent();

            //PlayerElement.TransportControls.IsFullWindowButtonVisible = false;
            //PlayerElement.TransportControls.IsFastRewindButtonVisible = false;
            //PlayerElement.TransportControls.IsZoomButtonVisible = false;
            //PlayerElement.TransportControls.IsCompact = true;
            //PlayerElement.TransportControls.IsEnabled = true;
            //PlayerElement.TransportControls.IsPlaybackRateButtonVisible = true;
            //       PlayerElement.TransportControls.Height = 48;
        }
    }
}
