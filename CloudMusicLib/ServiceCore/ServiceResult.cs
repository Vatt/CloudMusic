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
    public class ServiceResult<T>
    {
        public string ServiceName { get; set; }
        public T Result { get; set; }
        public ResultType Type { get; set; }
        public ServiceResult(string serviceName,ResultType type,T result)
        {
            ServiceName = serviceName;
            Result = result;
            Type = type;
        }
    }
}
