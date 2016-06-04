using CloudMusicLib.ServiceCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.CoreLibrary
{
    public class CloudTracklist
    {
        /*Constant - Статичный треклист, Dynamic треклист может подгружать данные*/
        public enum TracklistMode { Constant, Dynamic }
        public  List<CloudTrack> TracklistData { get; }
        private Dictionary<string, ServiceResultCollection<CloudTrack>> _serviceResultData;
        private TracklistMode Mode;
        public CloudTracklist(TracklistMode mode)
        {
            _serviceResultData = new Dictionary<string, ServiceResultCollection<CloudTrack>>();
            TracklistData = new List<CloudTrack>();
            Mode = mode;
        }
        public CloudTracklist(TracklistMode mode, Dictionary<string, ServiceResultCollection<CloudTrack>> servicesData)
        {
            Mode = mode;
            _serviceResultData = servicesData;
            foreach(var tracklist in _serviceResultData)
            {
                TracklistData.AddRange(tracklist.Value.Result);
            }
        }
        
        protected void MergeOther(string serviceName,List<CloudTrack> others)
        {
            _serviceResultData[serviceName].Result.AddRange(others);
            TracklistData.AddRange(others);
        }
        
        public void MergeOther(Dictionary<string, ServiceResultCollection<CloudTrack>> others)
        {
            foreach (var tracklist in others)
            {
                if (tracklist.Value.Type == ResultType.Ok)
                {
                    if (!_serviceResultData.ContainsKey(tracklist.Key))
                    {
                        _serviceResultData[tracklist.Key].Result = tracklist.Value.Result;
                    }
                    else
                    {
                        _serviceResultData[tracklist.Key].Result.AddRange(tracklist.Value.Result);
                    }
                    TracklistData.AddRange(tracklist.Value.Result);
                }
            }
        }
        public void MergeOther(ServiceResultCollection<CloudTrack> result)
        {
            if (result.Type == ResultType.Ok)
            {
                if (!_serviceResultData.ContainsKey(result.ServiceName))
                {
                    _serviceResultData[result.ServiceName] = result;
                }
                else
                {
                    _serviceResultData[result.ServiceName].Result.AddRange(result.Result);
                }
                TracklistData.AddRange(result.Result);
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
        public List<CloudTrack> LoadMoreIfPossible()
        {
            List<CloudTrack> items = new List<CloudTrack>(0);
            foreach(var result in _serviceResultData.Values)
            {
                if (result.IsIncrementalLoadingEnabled)
                {
                    items.AddRange(result.LoadNextIfPossible());
                    MergeOther(result.ServiceName, items);

                }
            }
            return items;
        }
        public async Task<List<CloudTrack>> LoadMoreIfPossibleAsync()
        {
            List<CloudTrack> items = new List<CloudTrack>(0);
            foreach (var result in _serviceResultData.Values)
            {
                if (result.IsIncrementalLoadingEnabled)
                {
                    items.AddRange(await result.LoadNextIfPossibleAsync());
                    MergeOther(result.ServiceName, items);

                }
            }
            return items;
        }
        public bool HaveMore()
        {
            foreach(var result in _serviceResultData.Values)
            {
                if (result.HasMore()) return true;
            }
            return false;
        }
    }
}
