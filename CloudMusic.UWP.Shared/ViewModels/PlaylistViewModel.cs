using System;
using System.Collections.Generic;
using System.Text;
using CloudMusic.UWP.Models;
using CloudMusic.UWP.ViewModels.Base;
using CloudMusicLib.CoreLibrary;
using CloudMusicLib.ServiceCore;
using System.Threading.Tasks;

namespace CloudMusic.UWP.ViewModels
{
    public class PlaylistViewModel:NotificationBase
    {
        public string PlaylistName { get; }
        private TracklistCollection tracklist { get; set; }
        public CloudPlaylist _original;
        public PlaylistViewModel(CloudPlaylist playlist)
        {
            PlaylistName = playlist.Name;
            _original = playlist;    
        }
        public async Task<TracklistCollection> GetTracklistAsync()
        {
            tracklist = new TracklistCollection(await _original.GetTrackListDataAsync());
            return tracklist;

        }
    }
}
