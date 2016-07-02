using CloudMusic.UWP.Common;
using CloudMusic.UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
using Windows.UI;
using Windows.UI.Core;
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
        private static SymbolIcon _pauseIcon = new SymbolIcon(Symbol.Pause);
        private static SymbolIcon _playIcon = new SymbolIcon(Symbol.Play);
        private static SolidColorBrush _onColor = new SolidColorBrush(Color.FromArgb(255, 130, 140, 152));
        private static SolidColorBrush _offColor = new SolidColorBrush(Color.FromArgb(0, 255, 255,255));
        public PlayerControl()
        {
            this.InitializeComponent();
            _vm = (PlayerControlViewModel)AppData.Get("PlayerControlViewModel");
            if (_vm == null)
            {
                AppData.Add("PlayerControlViewModel", new PlayerControlViewModel());
                _vm = (PlayerControlViewModel)AppData.Get("PlayerControlViewModel");
            }


            PlayerElement.TransportControls.IsSeekBarVisible = true;

            PlayerElement.TransportControls.IsCompact = true;
            PlayerElement.TransportControls.IsEnabled = true;
            PlayerElement.TransportControls.IsFullWindowButtonVisible = false;
            PlayerElement.TransportControls.IsFastRewindButtonVisible = false;
            PlayerElement.TransportControls.IsZoomButtonVisible = false;
            PlayerElement.TransportControls.IsPlaybackRateButtonVisible = false;
            PlayerElement.TransportControls.IsStopButtonVisible = false;
            PlayerElement.TransportControls.IsPlaybackRateButtonVisible = false;
            PlayerElement.TransportControls.IsVolumeButtonVisible = false;
            PlayerElement.TransportControls.IsFastRewindButtonVisible = false;

            GlobalEventSet.RegisterOrAdd("ActiveTrackChange", new Action<TrackViewModel>((track) => PlayPauseButton.Icon = _pauseIcon));
            this.Loaded += async (sender, args) =>
            {
                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    BackgroundMediaPlayer.SendMessageToBackground(new ValueSet());
                });
            };
        }
        private void PlayerElementPlayPause()
        {
            if(_vm.ActiveTrack == null) { return; }
            _vm.SwitchPlayPause();
            if(_vm.IsPaused)
            {
                PlayerElement.Pause();
                PlayPauseButton.Icon = _playIcon; 
            }
            else
            {
                PlayerElement.Play();
                PlayPauseButton.Icon = _pauseIcon;
            }
        }
        private void PlayerElementRepeate()
        {
            _vm.SwitchRepeateMode();
            if(_vm.IsRepeated)
            {
                RepeateOnOff.Background = _onColor;
            }else
            {
                RepeateOnOff.Background = _offColor;
            }
        }
        private void PlayerElementShuffle()
        {
            _vm.SwitchShuffleMode();
            if (_vm.IsShuffled)
            {
                ShuffleOnOff.Background = _onColor;
            }
            else
            {
                ShuffleOnOff.Background = _offColor;
            }
        }
    }
}
