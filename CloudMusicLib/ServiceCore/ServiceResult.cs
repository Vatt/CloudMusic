using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.ServiceCore
{
    public enum ResultType
    {
        Ok,
        Err,
    }
    public enum ResultMode
    {
        Lazy,
        Intense,
    }
    public class ServiceResult<T>
    {
        public string ServiceName { get; set; }
        public T Result { get; set; }
        public ResultType Type { get; set; }
        public ResultMode Mode { get; }
        public ServiceResult(string serviceName,ResultType type,T result)
        {
            ServiceName = serviceName;
            Result = result;
            Type = type;
            Mode = ResultMode.Intense;
        }
        public ServiceResult(string serviceName, ResultType type,ResultMode mode, T result)
        {
            ServiceName = serviceName;
            Result = result;
            Type = type;
            Mode = mode;
        }

    }
}
