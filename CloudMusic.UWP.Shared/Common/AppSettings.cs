using CloudMusicLib.ServiceCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage;
using Windows.Storage.Streams;

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
    class LocalEncryptDecryptHeper
    {

        public static async Task<string> EncryptAsync(string toEncrypt)
        {
            DataProtectionProvider Provider = new DataProtectionProvider("LOCAL=user");
            var encoding = BinaryStringEncoding.Utf8;
            IBuffer buffMsg = CryptographicBuffer.ConvertStringToBinary(toEncrypt, encoding);
            IBuffer buffProtected = await Provider.ProtectAsync(buffMsg);
            return CryptographicBuffer.EncodeToBase64String(buffProtected);
        }
        public static async Task<string> DecryptAsync(string toDecrypt)
        {
            DataProtectionProvider Provider = new DataProtectionProvider();
            var encoding = BinaryStringEncoding.Utf8;
            var buffer = CryptographicBuffer.DecodeFromBase64String(toDecrypt);
            IBuffer buffUnprotected = await Provider.UnprotectAsync(buffer);
            return  CryptographicBuffer.ConvertBinaryToString(encoding, buffUnprotected);
        }
    }
    public class LoginInfo
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string LastConnectedTime { get; set; }
    }
    public class AppConfig
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
            var task =  _roamingFolder.CreateFileAsync(_defines.ServicesConfFileName, CreationCollisionOption.ReplaceExisting);
            _servicesConfig = task.AsTask().Result;
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
            var encryptedUser = await LocalEncryptDecryptHeper.EncryptAsync(user);
            var encryptedPassword = await LocalEncryptDecryptHeper.EncryptAsync(password);
            serviceConf[_defines.ServiceLoginInfoGroupName] = new JObject
            {
                {_defines.ServiceLoginPropertyname, encryptedUser},
                {_defines.ServicePasswordPropertyName,encryptedPassword }
            };
            await FileIO.WriteTextAsync(_servicesConfig, configs.ToString());
        }
        public static async Task<LoginInfo> GetLoginInfo(string service)
        {
            JObject configs = JObject.Parse(await FileIO.ReadTextAsync(_servicesConfig));
            JToken serviceConf;
            if (!configs.TryGetValue(service, out serviceConf))
            {
                return null;
            }
            var loginInfoJson = serviceConf[_defines.ServiceLoginInfoGroupName];
            var user = await LocalEncryptDecryptHeper.DecryptAsync(loginInfoJson[_defines.ServiceLoginPropertyname].ToString());
            var password = await LocalEncryptDecryptHeper.DecryptAsync(loginInfoJson[_defines.ServicePasswordPropertyName].ToString());
            return new LoginInfo
            {
                User = user,
                Password = password,
                LastConnectedTime="",
            };
        }
    }
}
