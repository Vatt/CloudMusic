using System;
using System.Threading.Tasks;

namespace CloudMusicLib.ServiceCore
{
    internal abstract class ServiceMethod<TArgType,TOutType>
    {
        public readonly ServiceCommands Command;

        protected ServiceMethod(ServiceCommands command)
        {
            Command = command;
        }

        public abstract Task<TOutType> Invoke(TArgType[] args, params object[] extraArgs=null);
    }
}
