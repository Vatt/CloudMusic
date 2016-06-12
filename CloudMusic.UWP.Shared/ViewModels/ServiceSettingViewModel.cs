﻿using CloudMusic.UWP.Models;
using CloudMusic.UWP.ViewModels.Base;
using CloudMusicLib.ServiceCore;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.System;
using Windows.UI.Xaml.Controls;

namespace CloudMusic.UWP.ViewModels
{
    public class ServiceSettingViewModel : NotificationBase
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
            private set
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
            private set
            {
                SetProperty(ref _isAuthorized, value);
                _isAuthorized = value;
                if (_service.User != null)
                {
                    MessageIfAuthorized = $"Вы авторизованны:\n{_service.User.ToString()}";
                }
            }
        }
        public ServiceSettingViewModel(CloudService service)
        {
            _service = service;
            if (_service.User != null)
            {
                _messageIfAuthorized = $"Вы авторизованны:\n{_service.User.ToString()}";
                _isAuthorized = true;
            }
            else
            {
                _messageIfAuthorized = "";
                _isAuthorized = false;
            }

        }
        public string AdditionalMessage
        {
            get
            {
                return _service.AdditionalMessage;
            }
        }
        public async void Register()
        {
            await Launcher.LaunchUriAsync(_service.RegisterUri);
        }
        public async void TryLogin()
        {
            ServiceResult<DummyOutType> result = await CloudMan.InvokeCommandAsync<DummyOutType, string>
                                                (_service.ServiceName, ServiceCommands.Authorization, _login, _password);
            if(result.Type==ResultType.Err)
            {
                var dialog = new ContentDialog();
                dialog.Title = "Ошибка";
                dialog.Content = new TextBlock { Text = result.Text, };
                dialog.IsPrimaryButtonEnabled = true;
                dialog.PrimaryButtonText = "Ok";
                await dialog.ShowAsync();
            }
            else
            {
                IsAuthorized = true;
            }
        }
    
            
    }
}