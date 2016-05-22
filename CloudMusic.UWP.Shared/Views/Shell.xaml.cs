using CloudMusic.UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CloudMusicLib.ServiceCore;



namespace CloudMusic.UWP.Views
{
    
    public sealed partial class Shell : Page
    {
        public ShellViewModel ShellWM;
        public Shell()
        {            
            ShellWM = new ShellViewModel();
            this.InitializeComponent();
        }
    }
}
