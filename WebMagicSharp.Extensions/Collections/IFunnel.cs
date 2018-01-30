namespace WebMagicSharp.Collections
{
    public interface IFunnel<T>
    {
        void Funnel(T from, IPrimitiveSink into);
    }

}
