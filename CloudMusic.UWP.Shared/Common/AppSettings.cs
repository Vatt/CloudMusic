using CloudMusicLib.ServiceCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace CloudMusic.UWP.Common
{
    class ConfigDefines
    {
        public string ServicesConfFileName = "ServicesConfig.json";
        public string ServiceLoginInfoGroupName = "LoginInfo";
        public string ServiceAdditionalGroupName = "AdditionalInfo";
        public string ServiceLoginPropertyname = "Login";
        public string ServicePasswordPropertyName = "Password";
    }
    class AppConfig
    {
        private static  ApplicationDataContainer _localSettings;
        private static ApplicationDataContainer _roamingSettings;
        private static StorageFolder _localFolder;
        private static StorageFolder _roamingFolder;

        private static StorageFile _servicesConfig;

        private static ConfigDefines _defines;

        static AppConfig()
        {
            _localSettings = ApplicationData.Current.LocalSettings;
            _roamingSettings = ApplicationData.Current.RoamingSettings;
            _localFolder = ApplicationData.Current.LocalFolder;
            _roamingFolder = ApplicationData.Current.RoamingFolder;
            _defines = new ConfigDefines();
            ServicesConfigInit();



        }
        private static async void ServicesConfigInit()
        {
            try
            {
                _servicesConfig = await _roamingFolder.GetFileAsync(_defines.ServicesConfFileName);
            }
            catch (Exception)
            {
                await CreateServicesConfig();
            }
        }
        private static async Task CreateServicesConfig()
        {
            _servicesConfig = await _roamingFolder.CreateFileAsync(_defines.ServicesConfFileName, CreationCollisionOption.ReplaceExisting);
            JObject configs = new JObject();
            foreach(var service in CloudMan.Services())
            {
                configs[service.ServiceName] = CreateEmptyServiceConf();
            }
            await FileIO.WriteTextAsync(_servicesConfig, configs.ToString());
            
        }
        private static JObject CreateEmptyServiceConf()
        {
            return new JObject
            {
                { _defines.ServiceLoginInfoGroupName, new JObject() },
                { _defines.ServiceAdditionalGroupName, new JObject()}
            };
        }


        public static async void SaveLoginInfo(string service,string user,string password)
        {
            JObject configs = JObject.Parse(await FileIO.ReadTextAsync(_servicesConfig));
            JToken serviceConf;
            if (!configs.TryGetValue(service, out serviceConf))
            {
                configs[service] = CreateEmptyServiceConf();
                serviceConf = configs[service];
            }
            serviceConf[_defines.ServiceLoginInfoGroupName] = new JObject
            {
                {_defines.ServiceLoginPropertyname,user },
                {_defines.ServicePasswordPropertyName,password }
            };
            await FileIO.WriteTextAsync(_servicesConfig, configs.ToString());
        }

    }
}
