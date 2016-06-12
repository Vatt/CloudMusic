using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;

namespace CloudMusic.UWP.Common
{
    class AppSettings
    {
        public AppSettings()
        {
            var settings = ApplicationData.Current.LocalSettings;
            
        }
    }
}
