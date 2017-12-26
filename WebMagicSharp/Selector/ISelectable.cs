using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Selector
{
    /// <summary>
    /// Selectable.
    /// </summary>
    public interface ISelectable : IDisposable
    {

        /**
         * select list with xpath
         *
         * @param xpath xpath
         * @return new Selectable after extract
         */
        ISelectable Xpath(String xpath);

        ISelectable Jquery(String selector);

    /**
     * select list with css selector
     *
     * @param selector css selector expression
     * @param attrName attribute name of css selector
     * @return new Selectable after extract
     */
        ISelectable Jquery(String selector, String attrName);

        ISelectable Css(String selector);

        /**
         * select list with css selector
         *
         * @param selector css selector expression
         * @param attrName attribute name of css selector
         * @return new Selectable after extract
         */
        ISelectable Css(String selector, String attrName);

        /**
         * select smart content with ReadAbility algorithm
         *
         * @return content
         */
        ISelectable SmartContent();

        /**
         * select all links
         *
         * @return all links
         */
        ISelectable Links();

        /**
         * select list with regex, default group is group 1
         *
         * @param regex regex
         * @return new Selectable after extract
         */
        ISelectable Regex(String regex);

        /**
         * select list with regex
         *
         * @param regex regex
         * @param group group
         * @return new Selectable after extract
         */
        ISelectable Regex(String regex, int group);

        /**
         * replace with regex
         *
         * @param regex regex
         * @param replacement replacement
         * @return new Selectable after extract
         */
        ISelectable Replace(String regex, String replacement);

        /**
         * single string result
         *
         * @return single string result
         */
        String ToString();

        /**
         * single string result
         *
         * @return single string result
         */
        String Get();

        /**
         * if result exist for select
         *
         * @return true if result exist
         */
        bool Match();

        /**
         * multi string result
         *
         * @return multi string result
         */
        List<String> All();

        /**
         * extract by JSON Path expression
         *
         * @param jsonPath jsonPath
         * @return result
         */
        ISelectable JsonPath(String jsonPath);

        /**
         * extract by custom selector
         *
         * @param selector selector
         * @return result
         */
        ISelectable Select(ISelector selector);

        /**
         * extract by custom selector
         *
         * @param selector selector
         * @return result
         */
        ISelectable SelectList(ISelector selector);

        /**
         * get all nodes
         * @return result
         */
        List<ISelectable> Nodes();
    }
}
