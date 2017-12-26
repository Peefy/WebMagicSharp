using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Selector
{
    /// <summary>
    /// Selector.
    /// </summary>
    public interface ISelector : IDisposable
    {
        string Select(string text);

        List<string> SelectList(string text);
    }
}
