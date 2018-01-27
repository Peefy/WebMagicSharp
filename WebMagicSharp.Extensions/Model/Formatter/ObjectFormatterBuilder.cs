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
    public class ObjectFormatterBuilder<T> where T : IObjectFormatter
    {

        public static Type DefaultFormatterType => typeof(IObjectFormatter);

        private FieldInfo field;

        private PropertyInfo property;

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

        public ObjectFormatterBuilder<T> SetProperty(PropertyInfo property)
        {
            this.property = property;
            return this;
        }

        private IObjectFormatter InitFormatterForType(Type fieldType, string[] param) 
        {
            if (fieldType == typeof(string))
                return null;
            var formatterClass = ObjectFormatters<T>.
                Get(TypeUtil.DetectBasicType(fieldType));
            return Activator.CreateInstance<IObjectFormatter>();
        }

        private IObjectFormatter InitFormatter(Type formatterType, string[] param)
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

        public IObjectFormatter Build()
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
