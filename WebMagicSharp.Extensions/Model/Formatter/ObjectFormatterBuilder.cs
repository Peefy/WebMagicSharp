using System;
using System.Reflection;
using WebMagicSharp.Utils;
using WebMagicSharp.Model.Attributes;

namespace WebMagicSharp.Model.Formatter
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectFormatterBuilder<T> where T : class, IObjectFormatter<T>
    {

        public static Type DefaultFormatterType => typeof(IObjectFormatter<object>);

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

        private IObjectFormatter<T> InitFormatterForType(Type fieldType, string[] param) 
        {
            if (fieldType == typeof(string))
                return null;
            var formatterClass = ObjectFormatters<T>.
                Get(TypeUtil.DetectBasicType(fieldType));
            return Activator.CreateInstance<T>();
        }

        private IObjectFormatter<T> InitFormatter(Type formatterType, string[] param)
        {
            try
            {
                var objectFormatter = Activator.CreateInstance<T>();
                objectFormatter.InitParam(param);
                return objectFormatter;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IObjectFormatter<T> Build()
        {
            var formatter = AttributeUtil.GetAttribute<FormatterAttribute>(field);
            if (formatter?.Formatter.Equals(FormatterAttribute.DefaultFormatterType) == true)
            {
                return InitFormatter(formatter.Formatter, formatter.Value);
            }
            if (formatter == null && formatter.SubType.Equals(typeof(void)))
            {
                return InitFormatterForType(field.GetType(), formatter?.Value);
            }
            else
            {
                return InitFormatterForType(formatter.SubType, formatter.Value);
            }
        }

    }
}
