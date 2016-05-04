using System;
namespace CloudMusicLib.CoreLibrary
{
    public class CloudTrack
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public Uri TrackImage { get; set; }
        public Uri Source { get; set; }

    }
}
