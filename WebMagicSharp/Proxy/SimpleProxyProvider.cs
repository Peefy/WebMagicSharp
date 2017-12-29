using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Proxy
{
    public class SimpleProxyProvider : IProxyProvider
    {

        private List<Proxy> _proxies;

        private int _pointer = -1;

        public Proxy GetProxy(ITask task)
        {
            return _proxies[IncrForLoop];
        }

        public void ReturnProxy(Proxy proxy, Page page, ITask task)
        {
            
        }

        private SimpleProxyProvider()
        {

        }

        public SimpleProxyProvider(List<Proxy> proxies)
        {
            this._proxies = proxies;
        }

        private SimpleProxyProvider(List<Proxy> proxies, int pointer)
        {
            this._proxies = proxies;
            this._pointer = pointer;
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
                _pointer = _pointer + 1;
                int size = _proxies.Count;
                if (_pointer >= size)
                {
                    _pointer = 0;
                }
                return _pointer;
            }
        }
    }
}
