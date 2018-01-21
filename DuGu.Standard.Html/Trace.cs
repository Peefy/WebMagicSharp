// Description: Html Agility Pack - HTML Parsers, selectors, traversors, manupulators.
// Website & Documentation: http://html-agility-pack.net
// Forum & Issues: https://github.com/zzzprojects/html-agility-pack
// License: https://github.com/zzzprojects/html-agility-pack/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2017. All rights reserved.

namespace DuGu.Standard.Html
{
    internal class Trace
    {
        internal static Trace _current;

        internal static Trace Current
        {
            get
            {
                if (_current == null)
                    _current = new Trace();
                return _current;
            }
        }

        void WriteLineIntern(string message, string category)
        {
            System.Diagnostics.Debug.WriteLine(message, category);
        }

        public static void WriteLine(string message, string category)
        {
            Current.WriteLineIntern(message, category);
        }
    }
}