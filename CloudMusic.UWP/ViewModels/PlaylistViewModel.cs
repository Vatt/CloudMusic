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
        public Uri PlaylistImage { get; }
        public bool NoPlaylistImage { get; }
        private TracklistCollection tracklist { get; set; }
        public CloudPlaylist _original;
        public PlaylistViewModel(CloudPlaylist playlist)
        {
            _original = playlist;
            PlaylistName = playlist.Name;
            if (playlist.Image==null)
            {
                NoPlaylistImage = true;
            }else
            {
                NoPlaylistImage = false;
                PlaylistImage = playlist.Image;
            }

               
        }
        public string OwnerService()=> _original.ServiceSource;
        public async Task<TracklistCollection> GetTracklistAsync()
        {
            tracklist = new TracklistCollection(await _original.GetTrackListDataAsync());
            return tracklist;

        }
    }
}
