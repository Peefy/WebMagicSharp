using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Utils
{
    internal static class TypeUtil
    {

        public static List<FieldInfo> GetFieldsIncludeBaseClass(Type type)
        {
            var fields = new List<FieldInfo>();
            var currentType = type;
            while(currentType != null)
            {
                var currentFields = currentType.GetRuntimeFields();
                fields.AddRange(currentFields);
                currentType = currentType.BaseType;
            }
            return fields;
        }

        public static List<PropertyInfo> GetPropertysIncludeBaseClass(Type type)
        {
            var propertys = new List<PropertyInfo>();
            var currentType = type;
            while (currentType != null)
            {
                var currentFields = currentType.GetRuntimeProperties();
                propertys.AddRange(currentFields);
                currentType = currentType.BaseType;
            }
            return propertys;
        }

        public static Type DetectBasicType(Type type)
        {
            if (type == typeof(ushort))
                return typeof(ushort);
            else if (type == typeof(short))
                return typeof(short);
            else if (type == typeof(int))
                return typeof(int);
            else if (type == typeof(uint))
                return typeof(uint);
            else if (type == typeof(long))
                return typeof(long);
            else if (type == typeof(ulong))
                return typeof(ulong);
            else if (type == typeof(char))
                return typeof(char);
            else if (type == typeof(float))
                return typeof(float);
            else if (type == typeof(double))
                return typeof(double);
            else if (type == typeof(string))
                return typeof(string);
            else if (type == typeof(sbyte))
                return typeof(sbyte);
            else if (type == typeof(byte))
                return typeof(byte);
            else if (type == typeof(bool))
                return typeof(bool);
            else
                return type;
        }

        public static bool DetectIsBasicType(Type type)
        {
            if (type == typeof(ushort))
                return true;
            else if (type == typeof(short))
                return true;
            else if (type == typeof(int))
                return true;
            else if (type == typeof(uint))
                return true;
            else if (type == typeof(long))
                return true;
            else if (type == typeof(ulong))
                return true;
            else if (type == typeof(char))
                return true;
            else if (type == typeof(float))
                return true;
            else if (type == typeof(double))
                return true;
            else if (type == typeof(string))
                return true;
            else if (type == typeof(sbyte))
                return true;
            else if (type == typeof(byte))
                return true;
            else if (type == typeof(bool))
                return true;
            else
                return false;
        }
    }
}
