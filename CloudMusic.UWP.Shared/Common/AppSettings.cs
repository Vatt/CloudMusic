using CloudMusicLib.CoreLibrary;
using CloudMusicLib.ServiceCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
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
    public static class JsonExtensions
    {
        public static bool IsNullOrEmpty(this JToken token)
        {
            return (token == null) ||
                   (token.Type == JTokenType.Array && !token.HasValues) ||
                   (token.Type == JTokenType.Object && !token.HasValues) ||
                   (token.Type == JTokenType.String && token.ToString() == String.Empty) ||
                   (token.Type == JTokenType.Null);
        }
    }
    class ConfigDefines
    {
        public readonly string ServicesConfFileName = "ServicesConfig.json";
        public readonly string ServiceLoginInfoGroupName = "LoginInfo";
        public readonly string ServiceConnectInfoGroupName = "ConnectInfo";
        public readonly string ServiceUserInfoGroupName = "UserInfo";

        public readonly string ServiceIdPropertyName = "Id";
        public readonly string ServiceFirstnameProperty = "FirstName";
        public readonly string ServiceLastnameProperty = "LastName";
        public readonly string ServiceUserNamePropertry = "Username";

        public readonly string ServiceLoginPropertyname = "Login";
        public readonly string ServicePasswordPropertyName = "Password";


        public readonly string ServiceAccessTokenPropertyName = "access_token";
        public readonly string ServiceRefreshTokenPropertyName = "refresh_token";
        public readonly string ServiceExpiredInPropertyName = "expires_in";

    }

    public class LoginInfo
    {
        public string User { get; set; }
        public string Password { get; set; }
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
                { _defines.ServiceUserInfoGroupName, new JObject() }
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
            var encryptedPassword = await EncryptDecryptHeper.EncryptAsync(password);
            serviceConf[_defines.ServiceLoginInfoGroupName] = new JObject
            {
                {_defines.ServiceLoginPropertyname, user},
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
            if (loginInfoJson.IsNullOrEmpty())
            {
                return null;
            }
            var user = loginInfoJson[_defines.ServiceLoginPropertyname].ToString();
            var password = await EncryptDecryptHeper.DecryptAsync(loginInfoJson[_defines.ServicePasswordPropertyName].ToString());
            return new LoginInfo
            {
                User = user,
                Password = password
            };
        }

        public static async Task<bool> TryLoadUserInfo(CloudService service)
        {
            if (_servicesConfig==null)
            {
                return false;
            }
            JObject configs = JObject.Parse(await FileIO.ReadTextAsync(_servicesConfig));
            JToken serviceConf;
            if (!configs.TryGetValue(service.ServiceName, out serviceConf))
            {
                return false;
            }
            var userInfoJson = serviceConf[_defines.ServiceUserInfoGroupName];
            var loginInfo = await  GetLoginInfo(service.ServiceName);
            if (loginInfo==null)
            {
                return false;
            }
            var login = loginInfo.User;
            var id = (string)userInfoJson[_defines.ServiceIdPropertyName];
            var username = (string)userInfoJson[_defines.ServiceUserNamePropertry];
            var first_name = (string)userInfoJson[_defines.ServiceFirstnameProperty];
            var last_name = (string)userInfoJson[_defines.ServiceLastnameProperty];

            var user = new CloudUser(login);
            user.Login = login;
            user.UserName = username;
            user.FirstName = first_name;
            user.LastName = last_name;
            user.Id = id;
            service.SetUser(user);
            return true;
        }
        public static async Task SaveUserInfo(CloudService service)
        {
            string serviceName = service.ServiceName;
            CloudUser user = service.User;
            JObject configs = JObject.Parse(await FileIO.ReadTextAsync(_servicesConfig));
            JToken serviceConf;
            if (!configs.TryGetValue(serviceName, out serviceConf))
            {
                configs[service] = CreateEmptyServiceConf();
                serviceConf = configs[serviceName];
            }
            JObject userJson = new JObject
            {
                { _defines.ServiceIdPropertyName,user.Id },
                { _defines.ServiceLoginInfoGroupName,user.Login },
                { _defines.ServiceFirstnameProperty,user.FirstName },
                { _defines.ServiceLastnameProperty,user.LastName },
                {_defines.ServiceUserNamePropertry,user.UserName }
            };
            serviceConf[_defines.ServiceUserInfoGroupName] = userJson;
            await FileIO.WriteTextAsync(_servicesConfig, configs.ToString()).AsTask();
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
            JObject configs;
            string serviceName = service.ServiceName;
            if(_servicesConfig==null)
            {
                return false;
            }
            try
            {
                configs = JObject.Parse(await FileIO.ReadTextAsync(_servicesConfig));
            }
            catch (Exception)
            {
                return false;
            }
            JToken serviceConfJTok;  JObject serviceConf; JToken connectInfo; JObject ConnectJson;
            if (!configs.TryGetValue(serviceName, out serviceConfJTok))
            {
                return false;
            }
            serviceConf = (JObject)serviceConfJTok;
            if (!serviceConf.TryGetValue(_defines.ServiceConnectInfoGroupName, out connectInfo))
            {
                return false;
            }
            if((string)connectInfo[_defines.ServiceAccessTokenPropertyName]==null)return false;
            if ((string)connectInfo[_defines.ServiceRefreshTokenPropertyName] == null) return false;
            if ((string)connectInfo[_defines.ServiceExpiredInPropertyName] == null) return false;
            service.Connection.FromJsonString(connectInfo.ToString());
            return true;
        }
    }
}
