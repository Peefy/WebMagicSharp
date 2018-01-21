namespace DuGu.Standard.Http
{
    /// <summary>
    ///     返回类型
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        ///     表示只返回字符串
        /// </summary>
        String,

        /// <summary>
        ///     表示只返回字节流
        /// </summary>
        Byte,

        /// <summary>
        ///     急速请求,仅返回数据头
        /// </summary>
        So
    }

}
