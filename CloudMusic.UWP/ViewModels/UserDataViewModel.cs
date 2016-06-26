using CloudMusic.UWP.Common;
using CloudMusic.UWP.Models;
using CloudMusic.UWP.ViewModels.Base;
using CloudMusic.UWP.Views;
using CloudMusicLib.CoreLibrary;
using CloudMusicLib.ServiceCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace CloudMusic.UWP.ViewModels
{
    
    public class UserDataViewModel : NotificationBase
    {
        private PlaylistsCollection _userPlaylists;
 
        public PlaylistsCollection UserPlaylists {
            get
            {
                if (_userPlaylists == null)
                {
                    _userPlaylists = new PlaylistsCollection(CloudListMode.Dynamic);
                }
                return _userPlaylists;
            }
            set
            {
                SetProperty(ref _userPlaylists, value);
                _userPlaylists = value;
            }
        }
        public UserDataViewModel()
        {
            //_userPlaylists = new PlaylistsCollection(CloudListMode.Dynamic);
            Refresh();
        }
        public async void Refresh()
        {
            UserPlaylists = new PlaylistsCollection(CloudMan.GetUserPlaylists());
        }
        public void RemoveServiceData(string name)
        {
            RemovePlaylisrServiceData(name);
        }
        public void AddServiceData(string name)
        {
            AddPlaylistServiceData(name);
        }
        private void RemovePlaylisrServiceData(string name)
        {
            if (_userPlaylists == null) { return; }
            UserPlaylists.Remove((PlaylistViewModel pl) => pl.OwnerService().Equals(name));
        }
        private async void AddPlaylistServiceData(string name)
        {
            var result = await CloudMan.InvokeCommandAsync<List<CloudPlaylist>, DummyArgType>(name, ServiceCommands.GetUserPlaylists);
            Dictionary<string, ServiceResultCollection<CloudPlaylist>> data = new Dictionary<string, ServiceResultCollection<CloudPlaylist>>();
            data.Add(name, result as ServiceResultCollection<CloudPlaylist>);
            PlaylistsCollection collections;
            if (_userPlaylists == null) {

                var playlists = new CloudPlaylistList(CloudListMode.Dynamic);
                playlists.MergeOther(data);
                collections = new PlaylistsCollection(playlists);
                SetProperty(ref _userPlaylists, collections);
            } else
            {
                _userPlaylists._original.MergeOther(data);
                /*TODO: как добавлю дизер проверить вот этот кусок*/
                UserPlaylists = new PlaylistsCollection(_userPlaylists._original);
            }            
        }
    }
}
