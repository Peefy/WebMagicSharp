using System;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp;
using WebMagicSharp.Utils;
using WebMagicSharp.Model;
using WebMagicSharp.Model.Attributes;

namespace WebMagicSharp.Examples
{
    public class AppStoreExample
    {
        [ExtractBy(Type = ExtractType.JsonPath, Value = "$..trackName")]
        public string TrackName { get; set; } = "";

        [ExtractBy(Type = ExtractType.JsonPath, Value = "$..description")]
        public string Decription { get; set; } = "";

        [ExtractBy(Type = ExtractType.JsonPath, Value = "$..userRatingCount")]
        public int UserRatingCount { get; set; } 

        [ExtractBy(Type = ExtractType.JsonPath, Value = "$..screenshotUrls")]
        public List<string> ScreenshotUrls { get; set; } 

        [ExtractBy(Type = ExtractType.JsonPath, Value = "$..supportedDevices")]
        public List<string> SupportedDevices { get; set; }

        public AppStoreExample()
        {
            var appStore = OOSpider<AppStoreExample>.Create(Site.Me(), typeof(AppStoreExample)).
                Get<AppStoreExample>("http://itunes.apple.com/lookup?id=653350791&country=cn&entity=software");
        }

    }

}
