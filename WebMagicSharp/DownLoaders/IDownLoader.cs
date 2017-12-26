using System;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp;

namespace WebMagicSharp.DownLoaders
{
    public interface IDownLoader : IDisposable
    {
        Page DownLoad(Request request);
        void SetThread(int threadNum);

    }
}
