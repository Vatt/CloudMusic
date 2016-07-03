using System;
using CloudMusicLib.ServiceCore;

namespace CloudMusicLib.CoreLibrary
{
    public class CloudTrack
    {
        public string ServiceSource { get; set; }
        public string TrackName { get; set; }
        public string ArtistName { get; set; }
        public string AlbumName { get; set; }
        public DateTime Duration { get; set; }
        public CloudAlbum  Album { get; set; }
        public CloudArtist Artist { get; set; }
        public Uri TrackImage { get; set; }
        public Uri SourceData { get; set; }
        public override string ToString()
        {
            string data = "TrackName: " + TrackName + "\n" +
                          "ArtistName: " + ArtistName + "\n" +
                          "AlbumName: " + AlbumName + "\n" +
                          "ServiceSource: " + ServiceSource + "\n" +
                          "TrackImage: " + TrackImage + "\n" +
                          "TrackSourceData: " + SourceData + "\n\n";
            return data;
        }
    }
}
