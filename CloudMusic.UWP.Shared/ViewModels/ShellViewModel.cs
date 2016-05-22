using System;
using System.Collections.Generic;
using System.Text;
using CloudMusic.UWP.Models;
using CloudMusic.UWP.ViewModels.Base;
using CloudMusicLib.CoreLibrary;
using CloudMusicLib.ServiceCore;

namespace CloudMusic.UWP.ViewModels
{
    public class ShellViewModel:NotificationBase
    {
        public ServicesCollection Services;
        public PlaylistsCollection UserPlaylists;

        public ShellViewModel()
        {
            Services = new ServicesCollection();
            UserPlaylists = new PlaylistsCollection(CloudMan.InvokeCommand<IList<CloudPlaylist>, DummyArgType>("SoundCloud", ServiceCommands.GetUserPlaylists) );
        }
    }
}
