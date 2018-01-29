using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp.Utils;

namespace WebMagicSharp.Collections
{
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
            var newBitArray = classobj
            return new BloomFilter<T>()
        }

        public bool Apply(T input)
        {
            throw new NotImplementedException();
        }
    }

    public interface IPredicate<T>
    {
        bool Apply(T input);
        bool Equals(object obj);
    }

    public interface IFunnel<T>
    {
        void Funnel(T from, IPrimitiveSink into);
    }

    public interface IStrategy
    {
        bool Put<T, T1>(T obj, IFunnel<T1> funnel, int numHashFunctions, BitArray bits) where T1 : T;
        bool MightContain<T, T1>(T obj, IFunnel<T1> funnel, int numHashFunctions, BitArray bits) where T1 : T;
        int Ordinal { get; }
    }

    public interface IPrimitiveSink
    {
        IPrimitiveSink PutByte(byte b);
        IPrimitiveSink PutBytes(byte[] bytes);
        IPrimitiveSink PutBytes(byte[] bytes, int off, int len);
        IPrimitiveSink PutShort(short s);
        IPrimitiveSink PutInt(int i);
        IPrimitiveSink PutLong(long l);
        IPrimitiveSink PutFloat(float f);
        IPrimitiveSink PutDouble(double d);
        IPrimitiveSink PutBoolean(bool b);
        IPrimitiveSink PutChar(char c);
        IPrimitiveSink PutString(string str);
        IPrimitiveSink PutString(string str, Encoding encoding);
        IPrimitiveSink PutUnencodeedChars(string str);
        
    }

}
