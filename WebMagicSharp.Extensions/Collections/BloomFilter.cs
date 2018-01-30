using System;
using System.Collections;
using System.Collections.Generic;

using WebMagicSharp.Utils;

namespace WebMagicSharp.Collections
{
    [Obsolete("not finished")]
    [Serializable]
    public class BloomFilter<T> : IPredicate<T>
    {
        private readonly BitArray bits;

        private readonly int numHashFunctions;

        private readonly IFunnel<T> funnel;

        private readonly IStrategy strategy;

        private BloomFilter(BitArray bits, int numHashFunctions, IFunnel<T> funnel, IStrategy strategy)
        {
            ParameterUtil.CheckArgument(numHashFunctions > 0,"numHashFunctions (%s) must be > 0", 
                nameof(numHashFunctions));
            ParameterUtil.CheckArgument(numHashFunctions <= 255, "numHashFunctions (%s) must be > 0",
                nameof(numHashFunctions));
            this.bits = ParameterUtil.CheckNotNull(bits);
            this.numHashFunctions = numHashFunctions;
            this.funnel = ParameterUtil.CheckNotNull(funnel);
            this.strategy = ParameterUtil.CheckNotNull(strategy);
        }

        public BloomFilter<T> Copy()
        {
            var newBitArray = ClassObjectDeepCloneUtil.DeepCopyUsingXmlSerialize(bits);
            return new BloomFilter<T>(newBitArray, numHashFunctions, funnel, strategy);
        }

        public bool Apply(T input)
        {
            throw new NotImplementedException();
        }
    }

}
