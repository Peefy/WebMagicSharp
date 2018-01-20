using System;
using System.Collections.Generic;

namespace WebMagicSharp.Model.Formatter
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class ObjectFormatters<T> where T : class, IObjectFormatter<T>
    {
        private static Dictionary<Type, T> formatterMap
            = new Dictionary<Type, T>();

        public static void Put(T objectFormatter)
        {
            try
            {
                formatterMap.Add(typeof(T), objectFormatter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static T Get(Type type)
        {
            return formatterMap[type];
        }

    }
}
