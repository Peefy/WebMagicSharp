using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp
{
    /// <summary>
    /// Task.
    /// </summary>
    public interface ITask
    {
        Site GetSite();
        string GetGuid();
    }
}
