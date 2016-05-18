using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.CoreLibrary
{
    public class CloudTracklist
    {
        public IList<CloudTrack> TracklistData;

        public CloudTracklist()
        {
            TracklistData = new List<CloudTrack>();
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
