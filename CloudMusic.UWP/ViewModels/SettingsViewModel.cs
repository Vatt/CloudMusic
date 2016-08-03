using CloudMusic.UWP.Models;
using CloudMusic.UWP.ViewModels.Base;
using CloudMusicLib.ServiceCore;
using CloudMusic.UWP.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace CloudMusic.UWP.ViewModels
{
    public class SettingsViewModel:NotificationBase
    {
        private Pivot _pivot;
        public SettingsViewModel(Pivot pivot)
        {
            _pivot = pivot;
            foreach(var service in CloudMan.Services())
            {
                PivotItem newItem = new PivotItem();
                ServiceSettingViewModel model = new ServiceSettingViewModel(service);
                var control = new SettingControl();
                control.VM = model;
                
                newItem.Header = service.ServiceName;
                newItem.Content = control;
                _pivot.Items.Add(newItem);
            }
        }
    }
}
