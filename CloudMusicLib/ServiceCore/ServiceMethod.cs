using System;
using System.Threading.Tasks;

namespace CloudMusicLib.ServiceCore
{
    public class DummyArgType { }
    public class DummyOutType { }
    public abstract class ServiceMethod
    {
        public readonly ServiceCommands Command;
        protected readonly CloudService Service;
        protected ServiceMethod(CloudService service, ServiceCommands command)
        {
            Command = command;
            Service = service;
        }

        public abstract ServiceResult<TOutType> Invoke<TOutType, TArgType>(params TArgType[] args) where TOutType:class;

        public virtual Task<ServiceResult<TOutType>> InvokeAsync<TOutType, TArgType>(params TArgType[] args) where TOutType : class
        {
            return Task.Factory.StartNew(() => Invoke<TOutType, TArgType>());
        }
    }
}
