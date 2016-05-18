using System;
using System.Collections.Generic;
using System.Linq;
namespace CloudMusicLib.CoreLibrary
{
    public class CloudAlbum
    {
        public string AlbumName { get; set; }
        public CloudTracklist AlbumData { get; set; }
        public CloudArtist AlbumArtist { get; set; }
        public Uri AlbumImage { get; set; }
    }
}
