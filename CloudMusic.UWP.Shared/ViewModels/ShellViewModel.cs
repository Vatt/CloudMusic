using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Windows.UI.Xaml.Controls;
using CloudMusic.UWP.Models;
using CloudMusic.UWP.ViewModels.Base;
using CloudMusic.UWP.Views;
using CloudMusicLib.CoreLibrary;
using CloudMusicLib.ServiceCore;

namespace CloudMusic.UWP.ViewModels
{
    public class ShellViewModel:NotificationBase
    {
        public ServicesCollection Services;
        public PlaylistsCollection UserPlaylists { get; set; }
        public Frame WorkflowFrame;
        public ShellViewModel(Frame workflow)
        {
            Services = new ServicesCollection();
            ShellInit();
            WorkflowFrame = workflow;
            WorkflowFrame.Navigate(typeof(PlaylistsView), UserPlaylists);
        }

        public void ShellInit()
        {
            UserPlaylists = new PlaylistsCollection(CloudMan.InvokeCommand<IList<CloudPlaylist>, DummyArgType>("SoundCloud", ServiceCommands.GetUserPlaylists));
          //  Debug.WriteLine(UserPlaylists.ToString());
            
        }
    }
}
