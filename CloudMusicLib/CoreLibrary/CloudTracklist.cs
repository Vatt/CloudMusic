using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.CoreLibrary
{
    public class CloudTracklist
    {
        public List<CloudTrack> TracklistData;

        public CloudTracklist()
        {
            TracklistData = new List<CloudTrack>();
        }
        public void MergeOther(IList<CloudTracklist> others)
        {
            foreach(var tracklist in others)
            {
                TracklistData.AddRange(tracklist.TracklistData);
            }
        }
        public override string ToString()
        {
            string data="";
            foreach (var cloudTrack in TracklistData)
            {
                data += cloudTrack.ToString();
            }
            return data;
        }
    }
}
