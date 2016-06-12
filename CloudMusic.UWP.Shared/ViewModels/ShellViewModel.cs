using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Windows.UI.Xaml.Controls;
using CloudMusic.UWP.Models;
using CloudMusic.UWP.ViewModels.Base;
using CloudMusic.UWP.Views;
using CloudMusicLib.CoreLibrary;
using CloudMusicLib.ServiceCore;
using System.Windows.Input;

namespace CloudMusic.UWP.ViewModels
{
    public class ShellViewModel:NotificationBase
    {
        public bool IsServicesSplitViewPaneOpened;
        public ServicesCollection Services { get; set; }
        public Frame WorkflowFrame;


        public ShellViewModel(Frame workflow)
        {

            IsServicesSplitViewPaneOpened = true;
            Services = new ServicesCollection();
            ShellInit();
            WorkflowFrame = workflow;
            ToggleFavorites();
           
        }

        public async void ShellInit()
        {

            CloudMan.InvokeCommand<DummyOutType, string>("SoundCloud", ServiceCommands.Init,
                                                          "109f016fa8b98246e0e5156074389ff1",
                                                          "08b584be83dd9825488004bcee50e3b6");
            //CloudMan.InvokeCommand<DummyOutType, string>("SoundCloud", ServiceCommands.Authorization,
            //                                             "gamover-90@hotmail.com", "gam2106");
            
            //var tracklist = CloudMan.SearchTracks("Seceqtrique");
//            var tracklist = CloudMan.SearchTracks("Numb");
            //Debug.WriteLine(tracklist.ToString());

        }
        public void ToggleSearch()
        {
            WorkflowFrame.Navigate(typeof(Search));
        }
        public void ToggleFavorites()
        {
            WorkflowFrame.Navigate(typeof(UserData));
        }
        public void ToggleSettings()
        {
            WorkflowFrame.Navigate(typeof(SettingsView));
        }
    }
}
