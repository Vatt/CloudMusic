using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CloudMusic.UWP.ViewModels;
using CloudMusicLib.CoreLibrary;
using CloudMusicLib.ServiceCore;

namespace CloudMusic.UWP.Models
{
    public class PlaylistsCollection:ObservableCollection<PlaylistViewModel>
    {
        private IList<CloudPlaylist> _original;
        public PlaylistsCollection(IList<CloudPlaylist> lists )
        {
            _original = lists;
            foreach (var cloudPlaylist in lists)
            {
                base.Add(new PlaylistViewModel(cloudPlaylist));
            }
        }
    }
}
