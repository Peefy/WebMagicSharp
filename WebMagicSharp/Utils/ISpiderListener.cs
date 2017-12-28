using System;
namespace WebMagicSharp.Utils
{
    public interface ISpiderListener
    {
        void onSuccess(Request request);

        void onError(Request request);
    }
}
