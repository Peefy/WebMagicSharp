using System;
namespace WebMagicSharp.Utils
{
    public interface ISpiderListener
    {
        void OnSuccess(Request request);

        void OnError(Request request);
    }
}
