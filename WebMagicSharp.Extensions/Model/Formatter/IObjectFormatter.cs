using System;
using System.Reflection;
using System.Runtime;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Model.Formatter
{
    
    public interface IObjectFormatter<T> where T : class
    {
        T Format(string raw);

        Type Type { get; }

        void InitParam(string[] extra);
    }

    [Serializable]
    public class ObjectFormatters<T> where T : IObjectFormatter<T>
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

    public class ObjectFormatterBuilder<T> where T : IObjectFormatter<T>, new()
    {
        private FieldInfo field;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public ObjectFormatterBuilder<T> SetField(FieldInfo field)
        {
            this.field = field;
            return this;
        }

        private IObjectFormatter<T> initFormatterForType(Type type, string[] param) 
        {
            if (type == typeof(string))
                return null;
            return Activator.CreateInstance<T>();
        }
    }

    public class DateTimeFormatter
    {

    }

    public abstract class BaseTypeFormatter
    {

    }
       

}
