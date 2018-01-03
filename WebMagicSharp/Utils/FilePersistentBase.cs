using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Utils
{
    public class FilePersistentBase
    {
        protected string path;

        public static string PathSeparator => Path.DirectorySeparatorChar.ToString();

        public string GetFile(string fullName)
        {
            return Path.GetFullPath(fullName);
        }

    }
}
