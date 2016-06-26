using CloudMusic.UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.UWP.Common
{
    class AppData
    {
        private static Dictionary<string, object> _cache;
        static AppData()
        {
            _cache = new Dictionary<string, object>();
        }
        public static void Add(string name,object value)
        {
            
            _cache.Add(name, value);
        }
        public static object Get(string name)
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
        public static void Remove(string name)
        {
            if(_cache.ContainsKey(name))
            {
                _cache.Remove(name);
            }
        }
    }
}
