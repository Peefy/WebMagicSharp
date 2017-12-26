using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp
{
    public interface ITask
    {
        Site GetSite();
        string GetGuid();
    }
}
