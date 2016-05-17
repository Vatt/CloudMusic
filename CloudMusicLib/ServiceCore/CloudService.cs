using System.Collections.Generic;
namespace CloudMusicLib.ServiceCore
{
    public abstract class CloudService
    {
        public readonly string ServiceName;
        public ICloudConnection Connection;
        public bool IsAuthorizationRequired{ get; }
        public Dictionary<ServiceCommands, ServiceMethod> _commands;

        protected CloudService(string name, bool isAuthorizationRequired)
        {
            _commands = new Dictionary<ServiceCommands, ServiceMethod>();
            ServiceName = name;
            IsAuthorizationRequired = isAuthorizationRequired;
        }
        protected void AddMethod(ServiceMethod inMethod)
        {
            _commands.Add(inMethod.Command,inMethod);
        }
        public bool IsSupportedCommand(ServiceCommands inCommand)
        {
            return _commands.ContainsKey(inCommand);
        }

        public bool IsConnected()
        {
            return Connection.IsConnected();
        }

    }
}
