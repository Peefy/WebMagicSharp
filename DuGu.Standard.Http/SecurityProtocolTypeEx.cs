namespace DuGu.Standard.Http
{
    /// <summary>
    ///     Https时的传输协议.
    /// </summary>
    public enum SecurityProtocolTypeEx
    {
        // 摘要: 
        //     指定安全套接字层 (SSL) 3.0 安全协议。
        Ssl3 = 48,

        //
        // 摘要: 
        //     指定传输层安全 (TLS) 1.0 安全协议。
        Tls = 192,

        /// <summary>
        ///     指定传输层安全 (TLS) 1.1 安全协议。
        /// </summary>
        Tls11 = 768,

        /// <summary>
        ///     指定传输层安全 (TLS) 1.2 安全协议。
        /// </summary>
        Tls12 = 3072
    }

}
