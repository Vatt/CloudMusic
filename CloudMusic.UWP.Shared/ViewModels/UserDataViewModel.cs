using CloudMusic.UWP.Models;
using CloudMusic.UWP.ViewModels.Base;
using CloudMusic.UWP.Views;
using CloudMusicLib.CoreLibrary;
using CloudMusicLib.ServiceCore;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace CloudMusic.UWP.ViewModels
{
    public class UserDataViewModel : NotificationBase
    {
        PlaylistsCollection UserPlaylists { get; set; }
        Pivot _pivot;
        public UserDataViewModel(Pivot pivot)
        {
            _pivot = pivot;
            //UserPlaylists = new PlaylistsCollection(CloudMan.GetUserPlaylists());
            if (UserPlaylists != null)
            {
                MakePlaylistsPivotItem();
            }
        }
        //public void MakeTracksPivotItem()
        //{
        //    PivotItem newItem = new PivotItem();
        //    TracklistControl newControl = new TracklistControl();
        //    newControl.TrackListData =  Пользовательские треки сюда сунуть я должен;
        //    newItem.Header = "Треки";
        //    newItem.Content = newControl;
        //    _pivot.Items.Add(newItem);
        //}
        public void MakePlaylistsPivotItem()
        {
            PivotItem newItem = new PivotItem();
            PlaylistsControl newControl = new PlaylistsControl();
            newControl.Playlists = UserPlaylists;
            newItem.Header = "Плейлисты";
            newItem.Content = newControl;
            _pivot.Items.Add(newItem);
        }
    }
}
