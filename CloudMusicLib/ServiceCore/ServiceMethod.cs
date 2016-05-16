using System;
using System.Threading.Tasks;

namespace CloudMusicLib.ServiceCore
{
    public abstract class ServiceMethod
    {
        public readonly ServiceCommands Command;
        protected readonly CloudService Service;
        protected ServiceMethod(CloudService service, ServiceCommands command)
        {
            Command = command;
            Service = service;
        }

        public abstract TOutType Invoke<TOutType, TArgType>(params TArgType[] args) where TOutType:class;

        public virtual Task<TOutType> InvokeAsync<TOutType, TArgType>(params TArgType[] args) where TOutType : class
        {
            return Task.Factory.StartNew(() => Invoke<TOutType, TArgType>());
        }
    }
}
