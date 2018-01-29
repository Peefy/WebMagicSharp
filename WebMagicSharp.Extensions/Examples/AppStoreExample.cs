using System;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp;
using WebMagicSharp.Utils;
using WebMagicSharp.Model;
using WebMagicSharp.Model.Attributes;
using WebMagicSharp.Extensions;

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

        public static void Run()
        {
            var appStore = OOSpider.Run<AppStoreExample>
                ("http://itunes.apple.com/lookup?id=653350791&country=cn&entity=software");

            Console.WriteLine($"{nameof(appStore.TrackName)}:{appStore.TrackName}");
            Console.WriteLine($"{nameof(appStore.Decription)}:{appStore.Decription}");
            Console.WriteLine($"{nameof(appStore.UserRatingCount)}:{appStore.UserRatingCount}");
            Console.WriteLine($"{nameof(appStore.ScreenshotUrls)}:{appStore.ScreenshotUrls.ToListString()}");
            Console.WriteLine($"{nameof(appStore.SupportedDevices)}:{appStore.SupportedDevices.ToListString()}");

        }

    }

}
