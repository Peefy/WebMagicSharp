namespace DuGu.Standard.Http
{
    /// <summary>
    ///     Post的数据格式默认为string
    /// </summary>
    public enum PostDataType
    {
        /// <summary>
        ///     字符串
        /// </summary>
        String, //字符串

        /// <summary>
        ///     字节流
        /// </summary>
        Byte, //字符串和字节流

        /// <summary>
        ///     文件路径
        /// </summary>
        FilePath //表示传入的是文件
    }

}
