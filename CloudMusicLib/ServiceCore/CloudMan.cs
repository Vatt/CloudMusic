using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudMusicLib.ServiceCore
{
    public class CloudMan
    {
        private static Dictionary<string,CloudService> _services;

        static CloudMan()
        {
            _services = new Dictionary<string, CloudService>();
            RegisterService(new SoundCloudService.SoundCloudService());
        }
        public static void RegisterService(CloudService service)
        {
            _services.Add(service.ServiceName,service);
        }

        public static IList<CloudService> Services()
        {
            return _services.Values.ToList();
        }
        public static Task<IList<TOutType>> InvokeCommandAsync<TOutType, TArgType>(ServiceCommands command, params TArgType[] args) where TOutType : class
        {
            foreach (var service in _services.Values)
            {
            }
            return default(Task<IList<TOutType>>);
        }

        public static IList<TOutType> InvokeCommand<TOutType, TArgType>(ServiceCommands command, params TArgType[] args) where TOutType : class
        {
            foreach (var service in _services.Values)
            {
            }
            return default(IList<TOutType>);
        }

        public static Task<TOutType> InvokeCommandAsync<TOutType, TArgType>(string serviceName, ServiceCommands command, params TArgType[] args) where TOutType : class
        {
            if (_services[serviceName].IsSupportedCommand(command))
            {
                return _services[serviceName]._commands[command].InvokeAsync<TOutType, TArgType>(args);
            }
            return default(Task<TOutType>);
        }
        public static TOutType InvokeCommand<TOutType, TArgType>(string serviceName, ServiceCommands command, params TArgType[] args) where TOutType : class
        {
            TOutType result = default(TOutType);
            if (_services[serviceName].IsSupportedCommand(command))
            {
                result = _services[serviceName]._commands[command].Invoke<TOutType, TArgType>(args);
            }
            return result;
        }
    }
}
