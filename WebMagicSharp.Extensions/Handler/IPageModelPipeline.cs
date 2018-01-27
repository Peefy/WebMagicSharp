namespace WebMagicSharp.Handler
{
    public interface IPageModelPipeline<T>
    {
        void Process(T t, ITask task);
    }


}
