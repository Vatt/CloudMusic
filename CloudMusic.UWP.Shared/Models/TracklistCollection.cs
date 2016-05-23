using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CloudMusic.UWP.ViewModels;
using CloudMusicLib.CoreLibrary;

namespace CloudMusic.UWP.Models
{
    public class TracklistCollection : ObservableCollection<TrackViewModel>
    {
        private CloudTracklist _original;

        public TracklistCollection() : base()
        {
        }

        public TracklistCollection(CloudTracklist list)
        {
            _original = list;
            foreach (var track in _original.TracklistData)
            {
                base.Add(new TrackViewModel(track));
            }
        }
    }
}
