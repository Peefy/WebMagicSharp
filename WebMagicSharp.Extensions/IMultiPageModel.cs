using System;
using System.Collections.Generic;

namespace WebMagicSharp
{
    public interface IMultiPageModel
    {
        string GetPageKey();

        /**
         * page is the identifier of a page in pages for one object.
         *
         * @return page
         */
        string GetPage();

        /**
         * other pages to be extracted.<br>
         * It is used to judge whether an object contains more than one page, and whether the pages of the object are all extracted.
         *
         * @return other pages
         */
        ICollection<String> GetOtherPages();

        /**
         * Combine multiPageModels to a whole object.
         *
         * @param multiPageModel multiPageModel
         * @return multiPageModel combined
         */
        IMultiPageModel Combine(IMultiPageModel multiPageModel);
    }
}
