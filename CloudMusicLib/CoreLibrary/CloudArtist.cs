using System;
using System.Collections.Generic;
using System.Linq;
namespace CloudMusicLib.CoreLibrary
{
    public class CloudArtist
    {
        public string ArtistName { get; set;}
        IList<CloudAlbum> _albums;
    }
}
