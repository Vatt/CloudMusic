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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CloudMusic.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlaylistsView : Page
    {
        public PlaylistsCollection Playlists;
        public PlaylistsView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Playlists = (PlaylistsCollection) e.Parameter;
            base.OnNavigatedTo(e);
        }


        private void OnPlayListNameClick(object sender, ItemClickEventArgs e)
        {
            var playlistVm = (PlaylistViewModel) e.ClickedItem;
            this.PlaylistData.TrackListData = playlistVm.tracklist;
            this.PlaylistData.UpdateLayout();
        }
    }
}
