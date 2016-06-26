using CloudMusic.UWP.Common;
using CloudMusic.UWP.Models;
using CloudMusic.UWP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.UWP.ViewModels
{
    class PlayerControlViewModel : NotificationBase
    {
        public  TrackViewModel _activeTrack;
        private TracklistCollection _activeTracklist;
        public TrackViewModel ActiveTrack
        {
            get
            {
                return _activeTrack;
            }
            set
            {
                SetProperty(ref _activeTrack, value);
                //  _activeTrack = value;

            }
        }
        public TracklistCollection ActiveTracklist { get; set; }
        public PlayerControlViewModel()
        {
            GlobalEventSet.RegisterOrAdd("ActiveTrackChange", new Action<TrackViewModel>((track) => {
                ActiveTrack = track;
            }));
        }
    }
}
