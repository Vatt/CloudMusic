﻿using CloudMusic.UWP.ViewModels.Base;
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
        public TrackViewModel(CloudTrack inTrack,int index)
        {
            if (inTrack == null) return;
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
            if(inTrack.ArtistName==null||inTrack.ArtistName.Length==0)
            {
                ArtistName = "Unknown Artist";
            }
            Index = index;
        }
        public string TrackName { get; }
        public string ArtistName { get; }
        public string AlbumName { get; }
        public string Duration
        {
            get
            {
                var seconds = Track.Duration.Second;
                string seconds_string = seconds.ToString();
                if (seconds < 10)
                {
                    seconds_string = $"0{seconds_string}";
                }
                return $"{Track.Duration.Minute}:{seconds_string}";
            }
        }
        public Uri TrackImage { get; }
        public Uri SourceData { get; }
        public bool IsPlaying { get; set; }
        public int Index { get; set; }
    }
}
