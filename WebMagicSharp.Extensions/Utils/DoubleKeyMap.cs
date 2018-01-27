using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Utils
{
    internal class DoubleKeyMap<K1, K2, V> : MultiKeyMapBase
    {
        private Dictionary<K1, Dictionary<K2, V>> map;

        public DoubleKeyMap()
        {
            Init();
        }

        public DoubleKeyMap(Dictionary<K1, Dictionary<K2, V>> map) 
        {
            this.map = map;
        }

        private void Init()
        {
            if (map == null)
            {
                map = new Dictionary<K1, Dictionary<K2, V>>();
            }
        }

        public Dictionary<K2, V> Get(K1 key)
        {
            return map[key];
        }

        public V Get(K1 key1, K2 key2)
        {
            if (Get(key1) == null)
            {
                return default(V);
            }
            return Get(key1)[key2];
        }

        public V Put(K1 key1, Dictionary<K2, V> submap)
        {
            return Put(key1, submap);
        }

        public void Put(K1 key1, K2 key2, V value)
        {
            lock(this)
            {
                if (map[key1] == null)
                {
                    map.Add(key1, NewMap<K2, V>());
                }
                Get(key1).Add(key2, value);
            }
        }

        public void Remove(K1 key1, K2 key2)
        {          
            Get(key1).Remove(key2);
            Remove(key1);
        }

        public void Remove(K1 key1)
        {
            map.Remove(key1);
        }
    }
}
