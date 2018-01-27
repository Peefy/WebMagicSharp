using System;
using System.Runtime;
using System.Text;

using WebMagicSharp;

namespace WebMagicSharp.Model.Formatter
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IObjectFormatter
    {
        string Format(string raw);

        Type Type { get; }

        void InitParam(string[] extra);
    }
}
