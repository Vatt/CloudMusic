using CloudMusicLib.ServiceCore;
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
        private LazyLoad<CloudTracklist> LazyData;
        public CloudTracklist Data
        {
            get
            {
                return LazyData.Data;
            }
            set
            {
                LazyData.IntenseSet(value);               
            }
        }
        public CloudPlaylist(LazyLoad<CloudTracklist> lazy)
        {
            LazyData = lazy;
        }
        public CloudPlaylist()
        {
            LazyData.IntenseSet(new CloudTracklist(CloudListMode.Constant));
        }
        public override string ToString()
        {
            string data = "Playlist: " + Name + "\n" + Data.ToString();
            return data;
        }
    }
}
