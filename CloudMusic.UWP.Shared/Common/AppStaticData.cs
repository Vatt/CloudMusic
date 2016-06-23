using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.UWP.Common
{
    class AppStaticData
    {
        private static Dictionary<string, object> _cache;
        static AppStaticData()
        {
            _cache = new Dictionary<string, object>();
        }
        public static void AddToCache(string name,object value)
        {
            
            _cache.Add(name, value);
        }
        public static object GetFromCache(string name)
        {
            if (_cache.ContainsKey(name))
            {
                return _cache[name];
            }
            else
            {
                return null;
            }
        }
        public static void RemoveFromCache(string name)
        {
            if(_cache.ContainsKey(name))
            {
                _cache.Remove(name);
            }
        }
    }
}
