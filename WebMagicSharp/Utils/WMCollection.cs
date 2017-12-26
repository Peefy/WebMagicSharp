using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Utils
{
    public static class WMCollection<T>
    {

        public static HashSet<T> NewHashSet(T[] t)
        {
            var set = new HashSet<T>();
            foreach (var _t in t)
            {
                set.Add(_t);
            }
            return set;
        }

        public static List<T> NewList(T[] t)
        {
            var list = new List<T>();
            foreach(var _t in t )
            {
                list.Add(_t);
            }
            return list;
        }
    }
}
