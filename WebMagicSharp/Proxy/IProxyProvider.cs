using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Proxy
{
    public interface IProxyProvider
    {
        void ReturnProxy(Proxy proxy, Page page, ITask task);

        Proxy GetProxy(ITask task);
    }
}
