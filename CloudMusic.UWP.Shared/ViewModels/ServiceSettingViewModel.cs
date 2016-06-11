using CloudMusic.UWP.Models;
using CloudMusic.UWP.ViewModels.Base;
using CloudMusicLib.ServiceCore;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace CloudMusic.UWP.ViewModels
{
    public class ServiceSettingViewModel:NotificationBase
    {
        private CloudService _service { get; }
        private string _login;
        private string _password;
        private string _messageIfAuthorized;
        private bool _isAuthorized;
        public string Login {
            get
            {
                return _login;
            }
            set {
                SetProperty(ref _login, value);
            }
        }
        public string Password {
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
        }
        public bool IsAuthorized
        {
            get
            {
                return _isAuthorized;
            }
        }
        public ServiceSettingViewModel(CloudService service)
        {
            _service = service;
            if (_service.User != null)
            {
                _messageIfAuthorized = $"Вы авторизировались как: \n{_service.User.ToString()}";
                _isAuthorized = true;
            }
            else
            {
                _messageIfAuthorized = "";
                _isAuthorized = false;                
            }

        }
    }
}
