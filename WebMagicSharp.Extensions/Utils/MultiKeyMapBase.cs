using System;
using System.Collections.Generic;

namespace WebMagicSharp.Utils
{
    internal abstract class MultiKeyMapBase
    {
        public MultiKeyMapBase()
        {
        }

        protected Dictionary<T1,T2> NewMap<T1,T2>()
        {
            return new Dictionary<T1, T2>();
        }

    }
}
