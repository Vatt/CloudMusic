using CloudMusicLib.ServiceCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.CoreLibrary
{
    public class CloudPlaylist
    {
        public string ServiceSource;
        public string Name { get; set; }
        public Uri    Image { get; set; }
        private LazyLoad<CloudTracklist> LazyData;
        public void SetTracklist(CloudTracklist tracklist)
        {
            LazyData.IntenseSet(tracklist);
        }

        public CloudPlaylist(LazyLoad<CloudTracklist> lazy)
        {
            LazyData = lazy;
        }
        public CloudPlaylist()
        {
            LazyData = new Intense<CloudTracklist>() as LazyLoad<CloudTracklist>;
            LazyData.IntenseSet(new CloudTracklist(CloudListMode.Constant));
        }
        public async Task<CloudTracklist> GetTrackListDataAsync()
        {
            return await LazyData.GetDataAsync();
        }
        public  override string ToString()
        {
            string data = "Playlist: " + Name + "\n" + GetTrackListDataAsync().Result.ToString();
            return data;
        }
    }
}
