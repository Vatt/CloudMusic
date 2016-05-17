using System;
namespace CloudMusicLib.CoreLibrary
{
    public class CloudTrack
    {
        public string ServiceSource { get; set; }
        public string TrackName { get; set; }
        public string ArtistName { get; set; }
        public string Album { get; set; }
        public Uri TrackImage { get; set; }
        public Uri SourceData { get; set; }

    }
}
