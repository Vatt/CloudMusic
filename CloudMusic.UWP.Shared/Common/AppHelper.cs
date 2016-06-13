using System;
using System.Collections.Generic;
using System.Text;
using CloudMusicLib.ServiceCore;
namespace CloudMusic.UWP.Common
{
    public class AppHelper
    {
        public static async void CloudManInit()
        {
            foreach(var service in CloudMan.Services())
            {
                //TODO: Вот это возвращает bool
                await AppConfig.TryLoadServiceConnection(service);
            }
        }
    }
}
