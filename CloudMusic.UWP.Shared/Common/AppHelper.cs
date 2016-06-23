using System;
using System.Collections.Generic;
using System.Text;
using CloudMusicLib.ServiceCore;
using CloudMusic.UWP.ViewModels;


namespace CloudMusic.UWP.Common
{
    public class AppHelper
    {
        public static async void CloudManInit()
        {
            foreach(var service in CloudMan.Services())
            {
                //подписывается каждый сервис для сохранения коннекта
                service.Connection.OnConnectionChanged += async (CloudConnection con) =>
                 {
                     await AppConfig.SaveServiceInfo(service);
                 };
                //TODO: Вот это возвращает bool
                await AppConfig.TryLoadServiceConnection(service);
                await AppConfig.TryLoadUserInfo(service);
            }
            
        }
        public static void GlobalEventsInit()
        {

            GlobalEventSet.RegisterOrAdd("Logout", new Action<string>((string name) => {
                var vm = (UserDataViewModel)AppStaticData.GetFromCache("UserDataViewModel");
                vm.RemoveServiceData(name);
            }));
            GlobalEventSet.RegisterOrAdd("Login", new Action<string>((string name) => {
                var vm = (UserDataViewModel)AppStaticData.GetFromCache("UserDataViewModel");
                vm.AddServiceData(name);
            }));
        }
        public static void AppInit()
        {
            
        }
        
    }
}
