using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CloudMusic.UWP.ViewModels;
using CloudMusicLib.CoreLibrary;
using CloudMusicLib.ServiceCore;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace CloudMusic.UWP.Models
{
    public class PlaylistsCollection:ObservableCollection<PlaylistViewModel>, ISupportIncrementalLoading
    {
        public IList<CloudPlaylist> _original;
        public PlaylistsCollection(List<CloudPlaylist> lists )
        {
            _original = lists;
            foreach (var cloudPlaylist in lists)
            {
                base.Add(new PlaylistViewModel(cloudPlaylist));
            }
        }

        public bool HasMoreItems
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            string data = "";
            foreach (var cloudPlaylist in _original)
            {
                data += cloudPlaylist.ToString();
            }
            if (_original == null) data = "PlaylistsCollection is null!";
            return data;
        }
    }
}
