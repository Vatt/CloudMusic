using CloudMusicLib.CoreLibrary;
using System;
using System.Collections.Generic;
namespace CloudMusicLib.ServiceCore
{
    public abstract class CloudService
    {
        public readonly string ServiceName;
        public CloudUser User { get; protected set; }
        public ICloudConnection Connection;
        public Uri ServiceIcon { get; protected set; }
        public Uri RegisterUri { get; protected set; }
        public bool IsAuthorizationRequired{ get; }
        public string AdditionalMessage { get; protected set; }
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
        public bool IsSupportedCommand(ServiceCommands inCommand) => _commands.ContainsKey(inCommand);

        public bool IsConnected() => Connection.IsConnected();

        public void SetUser(CloudUser user) => User = user;

    }
}
