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
        public bool IsServicesSplitViewPaneOpened;
        public ServicesCollection Services { get; set; }
        public PlaylistsCollection UserPlaylists { get; set; }
        public Frame WorkflowFrame;
        public ShellViewModel(Frame workflow)
        {

            IsServicesSplitViewPaneOpened = true;
            Services = new ServicesCollection();
            ShellInit();
            WorkflowFrame = workflow;
            //WorkflowFrame.Navigate(typeof(PlaylistsView), UserPlaylists);
            WorkflowFrame.Navigate(typeof(Search));
        }

        public void ShellInit()
        {
            UserPlaylists = new PlaylistsCollection(CloudMan.InvokeCommand<IList<CloudPlaylist>, DummyArgType>("SoundCloud", ServiceCommands.GetUserPlaylists));
            var tracklist = CloudMan.SearchTracks("Seceqtrique");
            //Debug.WriteLine(tracklist.ToString());
            
        }
    }
}
