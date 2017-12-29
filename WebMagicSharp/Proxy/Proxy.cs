using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Proxy
{
    public class Proxy
    {
        private string _host;
        private int _port;
        private string _username;
        private string _password;

        public Proxy(String host, int port)
        {
            this._host = host;
            this._port = port;
        }

        public Proxy(String host, int port, String username, String password)
        {
            this._host = host;
            this._port = port;
            this._username = username;
            this._password = password;
        }

        public String Host => _host;

        public int Port => _port;

        public String Username => _username;

        public String Password => _password;

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null)
                return false;

            var proxy = obj as Proxy;
            if (_port != proxy._port) return false;
            if (_host != null ? !_host.Equals(proxy._host) : proxy._host != null) return false;
            if (_username != null ? !_username.Equals(proxy._username) : proxy._username != null) return false;
            return _password != null ? _password.Equals(proxy._password) : proxy._password == null;
        }

        public override int GetHashCode()
        {
            int result = _host != null ? _host.GetHashCode() : 0;
            result = 31 * result + _port;
            result = 31 * result + (_username != null ? _username.GetHashCode() : 0);
            result = 31 * result + (_password != null ? _password.GetHashCode() : 0);
            return result;
        }


        public override string ToString()
        {
            return "Proxy{" +
                    "host='" + _host + '\'' +
                    ", port=" + _port +
                    ", username='" + _username + '\'' +
                    ", password='" + _password + '\'' +
                    '}';
        }
    }
}
