using CloudMusic.UWP.Common;
using CloudMusic.UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CloudMusic.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserData : Page
    {
        
  
        private UserDataViewModel VM;
        public UserData()
        {
            this.InitializeComponent();
            VM = (UserDataViewModel)AppStaticData.GetFromCache("UserDataViewModel");
            if(VM==null)
            {
                AppStaticData.AddToCache("UserDataViewModel", new UserDataViewModel());
                VM = (UserDataViewModel)AppStaticData.GetFromCache("UserDataViewModel");
            }
            
        }
    }
}
