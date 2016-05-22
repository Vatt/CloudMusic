using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CloudMusic.UWP.ViewModels;
using CloudMusicLib.ServiceCore;

namespace CloudMusic.UWP.Models
{
    public class ServicesCollection:ObservableCollection<ServiceViewModel>
    {
        public ServicesCollection()
        {
            foreach (var service in CloudMan.Services())
            {
                base.Add(new ServiceViewModel(service));
            }
        }
    }
}
