using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.CoreLibrary
{
    public class CloudPlaylist
    {
        public string ServiceSource;
        public string Name { get; set; }
        public CloudTracklist Data;

        public CloudPlaylist()
        {
            Data = new CloudTracklist();
        }
        public override string ToString()
        {
            string data = "Playlist: " + Name + "\n" + Data.ToString();
            return data;
        }
    }
}
