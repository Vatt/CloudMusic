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
using CloudMusicLib.CoreLibrary;
using CloudMusicLib.ServiceCore;

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
               // TrackListData = value;
                this.TracklistView.ItemsSource = TrackListData;
                Debug.WriteLine(value.ToString());
                Debug.WriteLine(TrackListData.ToString());
                SetValue(TrackListDataProperty,value);
            }
        }
        private static void OnTrackListDataValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TracklistCollection NewData = (TracklistCollection)e.NewValue;
            TracklistControl control = (TracklistControl) d;
            control.TracklistView.ItemsSource = NewData;
        }
        public TracklistControl()
        {
            this.InitializeComponent();        
        }
    }
}
