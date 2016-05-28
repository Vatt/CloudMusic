using CloudMusic.UWP.Models;
using CloudMusic.UWP.ViewModels.Base;
using CloudMusic.UWP.Views;
using CloudMusicLib.CoreLibrary;
using CloudMusicLib.ServiceCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace CloudMusic.UWP.ViewModels
{
    public class SearchViewModel:NotificationBase
    {
        public bool SearchByTracks { get; set; }
        public bool SearchByArtists { get; set; }
        public bool SearchByAlbums { get; set; }
        public bool SearchByPlaylists { get; set; }

        public string SearchTemplate { get; set; }

        public bool IsSearchPaneOpened { get; set; }

        private Pivot _searchResultPivot;
        public SearchViewModel(Pivot inPivot)
        {
            _searchResultPivot = inPivot;
            IsSearchPaneOpened = true;
            SearchByTracks = true;
        }
        public async void RunSearch()
        {
            Task<CloudTracklist> tracksTask=null;
            _searchResultPivot.Items.Clear();
            if (SearchByTracks)
            {
                tracksTask =  CloudMan.SearchTracksAsync(SearchTemplate);
                MakeSearchTracksPivotItem(await tracksTask);
            }
           
        }
        public void MakeSearchTracksPivotItem(CloudTracklist inList)
        {
            PivotItem newItem = new PivotItem();
            TracklistControl newControl = new TracklistControl();
            newControl.TrackListData = new TracklistCollection(inList);
            newItem.Header = "Треки";
            newItem.Content =newControl;
            _searchResultPivot.Items.Add(newItem);
        }
    }
}
