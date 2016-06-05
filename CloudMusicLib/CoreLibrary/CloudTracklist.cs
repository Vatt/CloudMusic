using CloudMusicLib.ServiceCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.CoreLibrary
{
    //TODO: сделать ленивую загрузку
    public class CloudTracklist:CloudGenericList<CloudTrack>
    {
       
        public CloudTracklist(CloudListMode mode):base(mode)
        { }
        
        public CloudTracklist(CloudListMode mode, Dictionary<string, ServiceResultCollection<CloudTrack>> servicesData)
            :base(mode,servicesData)
        { }
        
       
        public override string ToString()
        {
            string data="";
            foreach (var cloudTrack in ListData)
            {
                data += cloudTrack.ToString();
            }
            return data;
        }
    }
}
