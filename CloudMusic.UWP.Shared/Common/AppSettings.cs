using CloudMusicLib.ServiceCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage;
using Windows.Storage.Streams;

namespace CloudMusic.UWP.Common
{
    class ConfigDefines
    {
        public readonly string ServicesConfFileName = "ServicesConfig.json";
        public readonly string ServiceLoginInfoGroupName = "LoginInfo";
        public readonly string ServiceConnectInfoGroupName = "ConnectInfo";
        public readonly string ServiceAdditionalGroupName = "AdditionalInfo";
        public readonly string ServiceLoginPropertyname = "Login";
        public readonly string ServicePasswordPropertyName = "Password";
        public readonly string ServiceAccessTokenPropertyName = "access_token";
        public readonly string ServiceRefreshTokenPropertyName = "refresh_token";
        public readonly string ServiceExpiredInPropertyName = "expired_in";

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
        private static JObject CreateEmptyServiceConf()
        {
            return new JObject
            {
                { _defines.ServiceLoginInfoGroupName,   new JObject() },
                { _defines.ServiceConnectInfoGroupName, new JObject() },
                { _defines.ServiceAdditionalGroupName,  new JObject() }
            };
        }
        private static async Task ServicesConfigInit()
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



        public static async Task SaveLoginInfo(string service,string user,string password)
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
            await FileIO.WriteTextAsync(_servicesConfig, configs.ToString()).AsTask();
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
        public static async Task SaveServiceInfo(CloudService service)
        {
            string serviceName = service.ServiceName;
            JObject connectInfo = JObject.Parse(service.Connection.ToJsonString());
            JObject configs = JObject.Parse(await FileIO.ReadTextAsync(_servicesConfig));
            JToken serviceConf;
            if (!configs.TryGetValue(serviceName, out serviceConf))
            {
                configs[service] = CreateEmptyServiceConf();
                serviceConf = configs[service];
            }
            serviceConf[_defines.ServiceConnectInfoGroupName] = connectInfo;
            await FileIO.WriteTextAsync(_servicesConfig, configs.ToString()).AsTask();
        }
        public static async Task<bool> TryLoadServiceConnection(CloudService service)
        {
            string serviceName = service.ServiceName;
            JObject configs = JObject.Parse(await FileIO.ReadTextAsync(_servicesConfig));
            JToken serviceConfJTok;  JObject serviceConf;
            JToken connectInfo;
            if (!configs.TryGetValue(serviceName, out serviceConfJTok))
            {
                return false;
            }
            serviceConf = (JObject)serviceConfJTok;
            if (!serviceConf.TryGetValue(_defines.ServiceConnectInfoGroupName, out connectInfo))
            {
                return false;
            }
            service.Connection.FromJsonString(connectInfo.ToString());
            return true;
        }
    }
}
