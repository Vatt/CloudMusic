using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using CloudMusic.UWP.Models;
using CloudMusic.UWP.ViewModels;
using CloudMusicLib.CoreLibrary;
using CloudMusicLib.ServiceCore;
using CloudMusic.UWP.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CloudMusic.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TracklistControl : UserControl
    {
        public static readonly DependencyProperty TrackListDataProperty =
            DependencyProperty.Register("TrackListData", typeof(TracklistCollection), typeof(TracklistControl),
                                        new PropertyMetadata(new TracklistCollection()));

        public TracklistCollection TrackListData
        {
            get
            {
                return (TracklistCollection)GetValue(TrackListDataProperty);
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                //this.TracklistView.ItemsSource = value;
                SetValue(TrackListDataProperty,value);
            }
        }

        public static readonly DependencyProperty SelectedTrackProperty =
            DependencyProperty.Register("SelectedTrack", typeof(TrackViewModel), typeof(TracklistControl),
                                        new PropertyMetadata(new TrackViewModel(null)));

        public TrackViewModel SelectedTrack
        {
            get
            {
                return (TrackViewModel)GetValue(SelectedTrackProperty);
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                SetValue(SelectedTrackProperty, value);
            }
        }


        public TracklistControl()
        {
            this.InitializeComponent();        
        }

        private void OnTrackClick(object sender, ItemClickEventArgs e)
        {
            var track = (TrackViewModel)e.ClickedItem;
            this.SelectedTrack = track;
            GlobalEventSet.Raise("ActiveTrackChange", track);
            
        }
    }
}
