﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Xml;
using HtmlAgilityPack;
using System.Linq;

namespace WebMagicSharp.Utils
{
    public class CharsetUtils
    {
        
        public static String DetectCharset(String contentType, byte[] contentBytes) 
        {
            String charset;
            charset = UrlUtils.GetCharset(contentType);
            if (String.IsNullOrEmpty(contentType) == false && 
                string.IsNullOrEmpty(charset) == false)
            {
                Debug.WriteLine($"Auto get charset: {charset}");
                return charset;
            }
            var content = Encoding.Default.GetString(contentBytes);
            if (string.IsNullOrEmpty(content) == false)
            {
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(content);
                var links = document.DocumentNode
                    .SelectNodes("meta");
                foreach(var link in links)
                {
                // 2.1、html4.01 <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
                    var metaContent = link.Attributes["content"].Value;
                    var metaCharset = link.Attributes["charset"].Value;
                    if (metaContent.IndexOf("charset") != -1)
                    {
                        metaContent = metaContent.Substring(
                            metaContent.IndexOf("charset"), metaContent.Length);
                        charset = metaContent.Split('=')[1];
                        break;
                    }
                    // 2.2、html5 <meta charset="UTF-8" />
                    else if (string.IsNullOrEmpty(metaCharset) == false)
                    {
                        charset = metaCharset;
                        break;
                    }
                }
            }
            Debug.WriteLine($"Auto get charset: {charset}");
            return charset;
        }
    }
}
