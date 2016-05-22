using CloudMusic.UWP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Web.Http.Headers;
using CloudMusicLib.CoreLibrary;

namespace CloudMusic.UWP.ViewModels
{
    public class TrackViewModel :NotificationBase
    {
        public CloudTrack Track;
        public bool NoTrackImage;
        public TrackViewModel(CloudTrack inTrack)
        {
            Track = inTrack;
            TrackName  = Track.TrackName;
            ArtistName = Track.ArtistName;
            AlbumName  = Track.AlbumName;
            SourceData = Track.SourceData;
            IsPlaying = false;
            if (Track.TrackImage == null)
            {
                NoTrackImage = true;
            }
            else
            {
                NoTrackImage = false;
                TrackImage = Track.TrackImage;
            }
        }
        public string TrackName { get; }
        public string ArtistName { get; }
        public string AlbumName { get; }
        public Uri TrackImage { get; }
        public Uri SourceData { get; }
        public bool IsPlaying { get; set; }
    }
}
