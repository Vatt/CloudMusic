using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.ServiceCore
{
    public abstract class ServiceResultCollection<T>:ServiceResult<List<T>>
    {
        public bool IsIncrementalLoadingEnabled;
        public ServiceResultCollection(string serviceName, ResultType type, List<T> result) : base(serviceName, type,result)
        {
        }
        public ServiceResultCollection(string serviceName, ResultType type,ResultMode mode, List<T> result) 
            : base(serviceName, type,mode, result)
        {
        }
        public ServiceResultCollection(ServiceResult<List<T>> result) 
            : base(result.ServiceName, result.Type,result.Mode, result.Result)
        {
        }
        public ServiceResult<List<T>> ToServiceResult()
        {
            return this;
        }
        public abstract List<T> LoadNextIfPossible();
        public abstract Task<List<T>> LoadNextIfPossibleAsync();
        public abstract bool HasMore();
    }
}
