using System;
using System.Collections.Generic;

namespace WebMagicSharp.Utils
{
    public abstract class MultiKeyMapBase<T1,T2>
    {
        public MultiKeyMapBase()
        {
        }

        protected Dictionary<T1,T2> NewMap()
        {
            return new Dictionary<T1, T2>();
        }

    }
}
