using CloudMusicLib.Common;
using CloudMusicLib.CoreLibrary;
using System;
using System.Collections.Generic;
namespace CloudMusicLib.ServiceCore
{
    public abstract class CloudService
    {
        public UserChangeEventHandler UserChangeHandler { get; set; }
        public event UserChangeEventHandler OnUserChanged;
        public void InvokeUserChange()
        {
            OnUserChanged?.Invoke(this);
        }
        public readonly string ServiceName;
        public CloudUser User { get; protected set; }
        public ConnectBaseInterface Connection;
        public Uri RegisterUri { get; protected set; }
        public bool IsAuthorizationRequired{ get; }
        public string RegisterLoginMessage { get; protected set; }
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
        ///<summary>
        //Для создания пользователя снаружи, например из приложения ранее сохранившего пользователя
        ///</summary>
        public void SetUser(CloudUser user) => User = user;

    }
}
