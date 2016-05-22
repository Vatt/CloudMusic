using System;
using System.Collections.Generic;
using System.Text;
using CloudMusic.UWP.ViewModels.Base;
using CloudMusicLib.ServiceCore;

namespace CloudMusic.UWP.ViewModels
{
    public class ServiceViewModel : NotificationBase
    {
        public string ServiceName {get;}
        public bool IsEnable { get; }
        public Uri ServiceIcon { get; }
        private CloudService _service;
        public ServiceViewModel(CloudService service)
        {
            _service = service;
            ServiceName = service.ServiceName;
            ServiceIcon = service.ServiceIcon;
            IsEnable = true;
        }
    }
}
