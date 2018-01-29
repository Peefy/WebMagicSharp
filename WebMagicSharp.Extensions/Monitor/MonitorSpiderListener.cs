using System.Collections.Generic;

using WebMagicSharp.Utils;

namespace WebMagicSharp.Monitor
{
    public class MonitorSpiderListener : ISpiderListener
    {

        List<string> errorUrls = new List<string>();

        public void OnError(Request request)
        {
            
        }

        public void OnSuccess(Request request)
        {
            
        }
    }

}
