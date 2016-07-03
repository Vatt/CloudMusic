using System;
using System.Collections.Generic;
using System.Text;
using CloudMusicLib.ServiceCore;
using CloudMusic.UWP.ViewModels;
using Windows.UI.Xaml.Media;

namespace CloudMusic.UWP.Common
{
    internal sealed class ServiceConfigFontIconDefines
    {
        /*SoundCloud FontIcon Conf*/
        public readonly string SoundCloudGlyph = "\uf1be;";
        public readonly double SoundCloudFontSize = 36;
        public readonly FontFamily SoundCloudFontFamily = new FontFamily("ms-appx:/Assets/IconFonts/fontawesome.ttf#FontAwesome");

        /*Deezer FontIcon Conf*/
    }
    public class AppHelper
    {
        private static ServiceConfigFontIconDefines _iconDefines = new ServiceConfigFontIconDefines();
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
            CloudMan.InvokeCommand<DummyOutType, string>("SoundCloud", ServiceCommands.Init,
                                                          "109f016fa8b98246e0e5156074389ff1",
                                                          "08b584be83dd9825488004bcee50e3b6");

        }
        public static void GlobalEventsInit()
        {

            GlobalEventSet.RegisterOrAdd("Logout", new Action<string>((string name) => {
                var vm = (UserDataViewModel)AppData.Get("UserDataViewModel");
                vm.RemoveServiceData(name);
            }));
            GlobalEventSet.RegisterOrAdd("Login", new Action<string>((string name) => {
                var vm = (UserDataViewModel)AppData.Get("UserDataViewModel");
                vm.AddServiceData(name);
            }));

        }
        public static void AppInit()
        {
            AppData.Add("PlayerControlViewModel", new PlayerControlViewModel());
        }
        public static Tuple<FontFamily,string,double> GetServiceFontIcon(string ServiceName)
        {
            switch (ServiceName)
            {
                case "SoundCloud":return new Tuple<FontFamily, string, double>(_iconDefines.SoundCloudFontFamily,
                                                                               _iconDefines.SoundCloudGlyph,
                                                                               _iconDefines.SoundCloudFontSize);
                default:return new Tuple<FontFamily, string, double>(null, null, 0);
            }
        }
    }
}
