using System;
using System.Collections.Generic;
using System.Text;
using CloudMusic.UWP.Models;
using CloudMusic.UWP.ViewModels.Base;
using CloudMusicLib.CoreLibrary;
using CloudMusicLib.ServiceCore;

namespace CloudMusic.UWP.ViewModels
{
    public class PlaylistViewModel:NotificationBase
    {
        public string PlaylistName { get; }
        public TracklistCollection tracklist { get; }
        public PlaylistViewModel(CloudPlaylist playlist)
        {
            PlaylistName = playlist.Name;
            tracklist = new TracklistCollection(playlist.Data);
        }
    }
}
