using CloudMusic.UWP.Common;
using CloudMusic.UWP.Models;
using CloudMusic.UWP.ViewModels.Base;
using CloudMusicLib.CoreLibrary;
using CloudMusicLib.DeezerService;
using CloudMusicLib.ServiceCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Windows.System;
using Windows.UI.Xaml.Controls;

namespace CloudMusic.UWP.ViewModels
{
    public class ServiceSettingViewModel : NotificationBase
    {
        public CloudService _service { get; }

        public Uri LoginUri;
        public WebView webView;
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
                _login = value;
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
                    MessageIfAuthorized = $"Вы авторизованы:\n{_service.User.ToString()}";
                }
            }
        }
        public ServiceSettingViewModel(CloudService service)
        {
            _service = service;
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
            if (service.WebBasedLogin)
            { 
                var con = _service.Connection as WebBasedLoginInterface;
                LoginUri = new Uri(con.LoginUrlString);
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
        public async void TryLogin()
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
        public async void Logout()
        {
            await AppConfig.RemoveLoginSettings(_service);
            _service.SetUser(null);
            _service.Connection.Disconnect();
            IsAuthorized = false;
            GlobalEventSet.Raise<string>("Logout", _service.ServiceName);

        }

        public void TryLoginWebViewBased()
        {
            WebView webView = new WebView();
            webView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;       
            webView.Navigate(LoginUri);
            webView.DOMContentLoaded += async (w, args) =>
            {
                await w.InvokeScriptAsync("eval", (_service.Connection as WebBasedLoginInterface).GetJSCallbacks(Login, Password));
               /* foreach (var callback in (_service.Connection as WebBasedLoginInterface).GetJSCallbacks(Login,Password)) {
                    await w.InvokeScriptAsync("eval", (_service.Connection as WebBasedLoginInterface).GetJSCallbacks(Login, Password));
                }*/                
            };
            webView.NavigationCompleted += (s, a) =>
              {
                  var conn = (_service.Connection as WebBasedLoginInterface);
                  conn.Response(webView.Source.AbsoluteUri);
              };
            


        }

    }
}
