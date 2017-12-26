using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Utils
{
    public class FilePersistentBase
    {
        protected String path;

        public static String PathSeparator => Path.DirectorySeparatorChar.ToString();

        public string GetFile(String fullName)
        {
            return Path.GetFullPath(fullName);
        }

    }
}
