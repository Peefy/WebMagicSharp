using System.Collections;

namespace WebMagicSharp.Collections
{
    public interface IStrategy
    {
        bool Put<T, T1>(T obj, IFunnel<T1> funnel, int numHashFunctions, BitArray bits) where T1 : T;
        bool MightContain<T, T1>(T obj, IFunnel<T1> funnel, int numHashFunctions, BitArray bits) where T1 : T;
        int Ordinal { get; }
    }

}
