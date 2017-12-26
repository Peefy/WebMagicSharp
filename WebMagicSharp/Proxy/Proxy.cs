using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Proxy
{
    public class Proxy
    {
        private string host;
        private int port;
        private string username;
        private string password;

        public Proxy(String host, int port)
        {
            this.host = host;
            this.port = port;
        }

        public Proxy(String host, int port, String username, String password)
        {
            this.host = host;
            this.port = port;
            this.username = username;
            this.password = password;
        }

        public String Host => host;

        public int Port => port;

        public String Username => username;

        public String Password => password;

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null)
                return false;

            var proxy = obj as Proxy;
            if (port != proxy.port) return false;
            if (host != null ? !host.Equals(proxy.host) : proxy.host != null) return false;
            if (username != null ? !username.Equals(proxy.username) : proxy.username != null) return false;
            return password != null ? password.Equals(proxy.password) : proxy.password == null;
        }

        public override int GetHashCode()
        {
            int result = host != null ? host.GetHashCode() : 0;
            result = 31 * result + port;
            result = 31 * result + (username != null ? username.GetHashCode() : 0);
            result = 31 * result + (password != null ? password.GetHashCode() : 0);
            return result;
        }


        public override string ToString()
        {
            return "Proxy{" +
                    "host='" + host + '\'' +
                    ", port=" + port +
                    ", username='" + username + '\'' +
                    ", password='" + password + '\'' +
                    '}';
        }
    }
}
