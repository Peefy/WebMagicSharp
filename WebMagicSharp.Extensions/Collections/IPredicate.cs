namespace WebMagicSharp.Collections
{
    public interface IPredicate<T>
    {
        bool Apply(T input);
        bool Equals(object obj);
    }

}
