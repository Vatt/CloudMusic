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
            WorkflowFrame = workflow;
            ToggleFavorites();
           
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
