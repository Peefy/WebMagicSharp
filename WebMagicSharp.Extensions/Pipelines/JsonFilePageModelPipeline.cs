using System;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;

using Newtonsoft.Json;

using WebMagicSharp.Model;
using WebMagicSharp.Pipelines;
using WebMagicSharp.Utils;

namespace WebMagicSharp.Pipelines
{
    public class JsonFilePageModelPipeline<T> : JsonFilePipeline, IPageModelPipeline<T>
    {

        public JsonFilePageModelPipeline()
        {
            this.path = "/data/webmagic/";
        }

        public JsonFilePageModelPipeline(string path)
        {
            this.path = path;
        }

        public void Process(T t, ITask task)
        {
            var totalPath = this.path + PathSeparator + task.GetGuid() + PathSeparator;
            try
            {
                var fileName = "";
                if (t is IHasKey o)
                {
                    fileName = totalPath + o.Key + ".json";
                }
                else
                {
                    fileName = totalPath + MD5.Create(t.ToString()) + ".json";
                }
                var jsonString = JsonConvert.SerializeObject(t);
                File.WriteAllText(fileName, jsonString);
            }
            catch (IOException e)
            {
                Debug.WriteLine("write file error:" + e);
            }
        }
    }

    public class JsonFilePageModelPipeline : JsonFilePipeline, IPageModelPipeline<object>
    {

        public JsonFilePageModelPipeline()
        {
            this.path = "/data/webmagic/";
        }

        public JsonFilePageModelPipeline(string path)
        {
            this.path = path;
        }

        public void Process(object t, ITask task)
        {
            var totalPath = this.path + PathSeparator + task.GetGuid() + PathSeparator;
            try
            {
                var fileName = "";
                if (t is IHasKey o)
                {
                    fileName = totalPath + o.Key + ".json";
                }
                else
                {
                    fileName = totalPath + MD5.Create(t.ToString()) + ".json";
                }
                var jsonString = JsonConvert.SerializeObject(t);
                File.WriteAllText(fileName, jsonString);
            }
            catch (IOException e)
            {
                Debug.WriteLine("write file error:" + e);
            }
        }
    }

}
