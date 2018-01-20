using System;
using System.Collections.Generic;

namespace WebMagicSharp
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMultiPageModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetPageKey();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetPage();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ICollection<String> GetOtherPages();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="multiPageModel"></param>
        /// <returns></returns>
        IMultiPageModel Combine(IMultiPageModel multiPageModel);
    }
}
