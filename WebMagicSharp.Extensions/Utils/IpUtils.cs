using System;
using System.Net;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Utils
{
    public class IpUtils
    {
        public static string FirstNoLoopbackIPAddresses
        {
            get
            {
                var ipReturn = IPAddress.Any;
                var host = new IPHostEntry();
                var ipList = host.AddressList;
                foreach(var ip in ipList)
                {
                    if (IPAddress.IsLoopback(ip) == false)
                        ipReturn = ip;
                }
                return ipReturn.ToString();
            }
        }
    }
}
