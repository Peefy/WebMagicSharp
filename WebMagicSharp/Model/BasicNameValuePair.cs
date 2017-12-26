using System;
using System.Text;
using WebMagicSharp.Utils;

namespace WebMagicSharp.Model
{
    public class BasicNameValuePair : INameValuePair
    {

        private readonly String name;
        private readonly String value;

        public BasicNameValuePair( String name,  String value)
        {
            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
            {
                throw null;
            }
            this.name = name;
            this.value = value;
        }

        public string GetName()
        {
            return name;
        }

        public string GetValue()
        {
            return value;
        }

        public override string ToString()
        {
            if (this.value == null)
            {
                return name;
            }
            int len = this.name.Length + 1 + this.value.Length;
            StringBuilder buffer = new StringBuilder(len);
            buffer.Append(this.name);
            buffer.Append("=");
            buffer.Append(this.value);
            return buffer.ToString();
        }

        public override bool Equals(object obj)
        {
            if (this is object)
            {
                return true;
            }
            if (obj is INameValuePair) {
                BasicNameValuePair that = (BasicNameValuePair)obj;
                return this.name.Equals(that.name)
                           && EqualsObj(this.value, that.value);
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = LangUtils.HASH_SEED;
            hash = LangUtils.hashCode(hash, this.name);
            hash = LangUtils.hashCode(hash, this.value);
            return hash;
        }

        bool EqualsObj( Object obj1,  Object obj2)
        {
            return obj1 == null ? obj2 == null : obj1.Equals(obj2);
        }

    }
}
