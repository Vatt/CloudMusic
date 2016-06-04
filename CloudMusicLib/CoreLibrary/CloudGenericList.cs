using CloudMusicLib.ServiceCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.CoreLibrary
{
    public enum CloudListMode { Constant, Dynamic }
    public abstract class CloudGenericList<T>
    {
        public List<T> ListData { get; }
        protected Dictionary<string, ServiceResultCollection<T>> _serviceResultData;
        protected CloudListMode Mode;
        public CloudGenericList(CloudListMode mode)
        {
            Mode = mode;
            _serviceResultData = new Dictionary<string, ServiceResultCollection<T>>();
            ListData = new List<T>();
        }
        public CloudGenericList(CloudListMode mode, Dictionary<string, ServiceResultCollection<T>> servicesData)
        {
            Mode = mode;
            _serviceResultData = servicesData;
            foreach (var tracklist in _serviceResultData)
            {
                ListData.AddRange(tracklist.Value.Result);
            }
        }
        protected void MergeOther(string serviceName, List<T> others)
        {
            _serviceResultData[serviceName].Result.AddRange(others);
            ListData.AddRange(others);
        }
        public void MergeOther(Dictionary<string, ServiceResultCollection<T>> others)
        {
            foreach (var item in others)
            {
                if (item.Value.Type == ResultType.Ok)
                {
                    if (!_serviceResultData.ContainsKey(item.Key))
                    {
                        _serviceResultData[item.Key].Result = item.Value.Result;
                    }
                    else
                    {
                        _serviceResultData[item.Key].Result.AddRange(item.Value.Result);
                    }
                    ListData.AddRange(item.Value.Result);
                }
            }
        }
        public void MergeOther(ServiceResultCollection<T> result)
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
                ListData.AddRange(result.Result);
            }
        }
        public bool HaveMore()
        {
            if (Mode == CloudListMode.Constant) return false;
            foreach (var result in _serviceResultData.Values)
            {
                if (result.HasMore()) return true;
            }
            return false;
        }
        public List<T> LoadMoreIfPossible()
        {
            List<T> items = new List<T>(0);
            if (Mode == CloudListMode.Constant) return items;
            foreach (var result in _serviceResultData.Values)
            {
                if (result.IsIncrementalLoadingEnabled)
                {
                    items.AddRange(result.LoadNextIfPossible());
                    MergeOther(result.ServiceName, items);

                }
            }
            return items;
        }
        public async Task<List<T>> LoadMoreIfPossibleAsync()
        {
            List<T> items = new List<T>(0);
            if (Mode == CloudListMode.Constant) return items;
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
    }
}
