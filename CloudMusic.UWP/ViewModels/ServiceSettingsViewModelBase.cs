using CloudMusic.UWP.ViewModels.Base;
using CloudMusicLib.ServiceCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace CloudMusic.UWP.ViewModels
{
    abstract class ServiceSettingsViewModelBase:NotificationBase
    {
        public CloudService _service { get; set; }
        protected string _login;
        protected string _password;
        protected string _messageIfAuthorized;
        protected bool _isAuthorized;
        public ServiceSettingsViewModelBase(CloudService service)
        {
            _service = service;
        }
        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                SetProperty(ref _login, value);
                _login = value;
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                SetProperty(ref _password, value);
            }
        }
        public string MessageIfAuthorized
        {
            get
            {
                return _messageIfAuthorized;
            }
            protected set
            {
                SetProperty(ref _messageIfAuthorized, value);
                _messageIfAuthorized = value;
            }
        }
        public bool IsAuthorized
        {
            get
            {
                return _isAuthorized;
            }
            protected set
            {
                SetProperty(ref _isAuthorized, value);
                _isAuthorized = value;
                if (_service.User != null)
                {
                    MessageIfAuthorized = $"Вы авторизованы:\n{_service.User.ToString()}";
                }
            }
        }

        public string AdditionalMessage
        {
            get
            {
                return _service.RegisterLoginMessage;
            }
        }
        public bool AdditionalMessageVisible
        {
            get
            {
                return AdditionalMessage.Length > 0;
            }
        }
        public async void Register()
        {
            await Launcher.LaunchUriAsync(_service.RegisterUri);
        }
        public abstract void TryLogin();
        public abstract void Logout();
    }
}
