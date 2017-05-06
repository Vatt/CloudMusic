using System;
using System.Collections.Generic;
using System.Text;
using CloudMusicLib.ServiceCore;
using CloudMusic.UWP.ViewModels;
using Windows.UI.Xaml.Media;
using CloudMusicLib.CoreLibrary;

namespace CloudMusic.UWP.Common
{
    internal sealed class ServiceConfigFontIconDefines
    {
        /*SoundCloud FontIcon Conf*/
        public readonly string SoundCloudGlyph = "\uf1be;";
        public readonly double SoundCloudFontSize = 36;
        public readonly FontFamily SoundCloudFontFamily = new FontFamily("ms-appx:/Assets/IconFonts/fontawesome.ttf#FontAwesome");

        /*Deezer FontIcon Conf*/
        public readonly string DeezerGlyph = "a";
        public readonly double DeezerFontSize = 36;
        public readonly FontFamily DeezerFontFamily = new FontFamily("ms-appx:/Assets/IconFonts/deezer.ttf#deezer");
    }
    public class AppHelper
    {
        private static ServiceConfigFontIconDefines _iconDefines = new ServiceConfigFontIconDefines();
        public static async void CloudManInit()
        {
            foreach(var service in CloudMan.Services())
            {
                //подписывается каждый сервис для сохранения коннекта
                service.Connection.OnConnectionChanged += async (ConnectBaseInterface con) =>
                 {
                     await AppConfig.SaveServiceInfo(service);
                 };
                service.OnUserChanged += async (CloudService s) =>
                {
                    await AppConfig.SaveUserInfo(s);
                };
                //TODO: Вот это возвращает bool
                await AppConfig.TryLoadServiceConnection(service);
                await AppConfig.TryLoadUserInfo(service);
            }
            CloudMan.InvokeCommand<DummyOutType, string>("SoundCloud", ServiceCommands.Init,
                                                          "109f016fa8b98246e0e5156074389ff1",
                                                          "08b584be83dd9825488004bcee50e3b6");
            // FUCK YOU, DEEZER
            /*CloudMan.InvokeCommand<DummyOutType, string>("Deezer", ServiceCommands.Init, "184342",
                                                         "3051aef8c1554a4ce5a062e31d117179", 
                                                         "https://connect.deezer.com/");*/
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
                case "Deezer":
                    return new Tuple<FontFamily, string, double>(_iconDefines.DeezerFontFamily,
                                                                 _iconDefines.DeezerGlyph,
                                                                 _iconDefines.DeezerFontSize);
                default:return new Tuple<FontFamily, string, double>(null, null, 0);
            }
        }
    }
}
