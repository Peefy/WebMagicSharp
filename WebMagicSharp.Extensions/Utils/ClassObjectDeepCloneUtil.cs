using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace WebMagicSharp.Utils
{

    internal static class ClassObjectDeepCloneUtil
    {
        public static T DeepCopyUsingReflection<T>(T obj)
        {
            if (obj is string || obj.GetType().IsValueType) return obj;
            var retval = Activator.CreateInstance(obj.GetType());
            var fields = obj.GetType().GetFields(bindingAttr: BindingFlags.Public | 
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                try { field.SetValue(retval, DeepCopyUsingReflection(field.GetValue(obj))); }
                catch { }
            }
            return (T)retval;
        }

        public static T DeepCopyUsingXmlSerialize<T>(T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                xml.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = xml.Deserialize(ms);
                ms.Close();
            }
            return (T)retval;
        }

        public static T DeepCopyUsingBinSerialize<T>(T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                //序列化成流
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                //反序列化成对象
                retval = bf.Deserialize(ms);
                ms.Close();
            }
            return (T)retval;
        }

    }
}
