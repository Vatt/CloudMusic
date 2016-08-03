using CloudMusic.UWP.Common;
using CloudMusic.UWP.Models;
using CloudMusic.UWP.ViewModels.Base;
using CloudMusicLib.CoreLibrary;
using CloudMusicLib.DeezerService;
using CloudMusicLib.ServiceCore;
using CloudMusicLib.SoundCloudService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Windows.System;
using Windows.UI.Xaml.Controls;

namespace CloudMusic.UWP.ViewModels.ServiceViewModels
{
    class ScSettingBaseViewModel : ServiceSettingsViewModelBase
    {
        public ScSettingBaseViewModel(CloudService service):base(service)
        {
           
            if (_service.User != null)
            {
                _messageIfAuthorized = $"Вы авторизованы:\n{_service.User.ToString()}";
                _isAuthorized = true;
                return;
            }
            else
            {
                _messageIfAuthorized = "";
                _isAuthorized = false;
            }
        }



        public override async void TryLogin()
        {
            ServiceResult<CloudUser> result = await CloudMan.InvokeCommandAsync<CloudUser, string>
                                                (_service.ServiceName, ServiceCommands.Authorization, _login, _password);
            if (result.Type == ResultType.Err)
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
                _service.SetUser(result.Result);
                await AppConfig.SaveLoginInfo(_service.ServiceName, _login, _password);
                await AppConfig.SaveServiceInfo(_service);
                await AppConfig.SaveUserInfo(_service);
                GlobalEventSet.Raise("Login", _service.ServiceName);
            }
        }
        public override async void Logout()
        {
            await AppConfig.RemoveLoginSettings(_service);
            _service.SetUser(null);
            _service.Connection.Disconnect();
            IsAuthorized = false;
            GlobalEventSet.Raise<string>("Logout", _service.ServiceName);

        }

 

    }
}
