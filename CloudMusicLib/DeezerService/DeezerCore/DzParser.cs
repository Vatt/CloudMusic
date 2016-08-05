using CloudMusicLib.CoreLibrary;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.DeezerService.DeezerCore
{
    class DzParser
    {
        public static string[] ParseFragment(string fragment)
        {
            string[] splited = fragment.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            string[] expiresPart = splited[1].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            string[] accessTokPart = splited[0].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            string expires = expiresPart[1];
            string accessTok = accessTokPart[1];
            return new string[] { accessTok, expires };
        }
        public static CloudUser ParserUserInfo(string jsonString)
        {
            JObject json   = JObject.Parse(jsonString);
            CloudUser user = new CloudUser(DzApi.service,(string)json["email"]);
            user.FirstName = (string)json["firstname"];
            user.LastName  = (string)json["lastname"];
            user.Id        = (string)json["id"];
            user.UserName  = (string)json["name"];
            return user; 
        }
    }
}
