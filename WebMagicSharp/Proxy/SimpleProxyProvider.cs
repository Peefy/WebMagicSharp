using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Proxy
{
    public class SimpleProxyProvider : IProxyProvider
    {

        private List<Proxy> proxies;

        private int pointer = -1;

        public Proxy GetProxy(ITask task)
        {
            return proxies[IncrForLoop];
        }

        public void ReturnProxy(Proxy proxy, Page page, ITask task)
        {
            
        }

        private SimpleProxyProvider()
        {

        }

        public SimpleProxyProvider(List<Proxy> proxies)
        {
            this.proxies = proxies;
        }

        private SimpleProxyProvider(List<Proxy> proxies, int pointer)
        {
            this.proxies = proxies;
            this.pointer = pointer;
        }

        public static SimpleProxyProvider From(IList<Proxy> proxies)
        {
            List<Proxy> proxiesTemp = new List<Proxy>();
            foreach (var proxy in proxies)
            {
                proxiesTemp.Add(proxy);
            }
            return new SimpleProxyProvider(proxiesTemp);
        }

        private int IncrForLoop
        {
            get
            {
                pointer = pointer + 1;
                int size = proxies.Count;
                if (pointer >= size)
                {
                    pointer = 0;
                }
                return pointer;
            }
        }
    }
}
