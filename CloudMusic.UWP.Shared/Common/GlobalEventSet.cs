using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CloudMusic.UWP.Common
{

    internal sealed class GlobalEventSet
    {
        public static Dictionary<string, Delegate> _table = new Dictionary<string, Delegate>();

        public static void TryGet(string key,out Delegate handler)
        {
            _table.TryGetValue(key, out handler);
        }
        public static void RegisterOrAdd(string key, Delegate handler)
        {
            Delegate d;
            _table.TryGetValue(key, out d);
            _table[key] = Delegate.Combine(d, handler);
        }
        public static void Remove(string key, Delegate handler)
        {
            Delegate d;
            if (_table.TryGetValue(key, out d))
            {
                d = Delegate.Remove(d, handler);
                if (d != null)
                {
                    _table[key] = d;
                }
                else
                {
                    _table.Remove(key);
                }
            }
        }
        public static void Raise<T>(string key, T arg)
        {
            Delegate d;
            _table.TryGetValue(key, out d);
            if (d != null)
            {
                d.DynamicInvoke(arg);
            }
        }
        public static void Raise(string key, object arg)
        {
            Delegate d;
            _table.TryGetValue(key, out d);
            if (d != null)
            {
                d.DynamicInvoke(arg);
            }
        }
        public static void Raise(string key, params object[] args)
        {
            Delegate d;
            _table.TryGetValue(key, out d);
            if (d != null)
            {
                d.DynamicInvoke(args);
            }
        }

    }

} 

