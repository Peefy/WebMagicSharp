using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace WebMagicSharp.Utils
{
    internal static class AttributeUtil
    {
        public static T GetAttribute<T>(MemberInfo member) where T : Attribute
        {
            return member.GetCustomAttribute(typeof(T), false) as T;
        }
    }
}
