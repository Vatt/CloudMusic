using System;
using System.Collections.Generic;
using System.Text;
using CloudMusic.UWP.ViewModels.Base;
using CloudMusicLib.ServiceCore;
using Windows.UI.Xaml.Media;
using CloudMusic.UWP.Common;

namespace CloudMusic.UWP.ViewModels
{
    public class ServiceViewModel : NotificationBase
    {
        public string ServiceName {get;}
        public bool IsEnable { get; }
        private CloudService _service;
        public FontFamily ServiceFontFamily { get; set; }
        public string ServiceGlyph { get; set; }
        public double ServiceFontSize { get; set; }
        public ServiceViewModel(CloudService service)
        {
            _service = service;
            ServiceName = service.ServiceName;
            //TODO: взять из настроек
            IsEnable = true;
            var icon = AppHelper.GetServiceFontIcon(service.ServiceName);
            ServiceFontFamily = icon.Item1;
            ServiceGlyph = icon.Item2;
            ServiceFontSize = icon.Item3;
        }
    }
}
