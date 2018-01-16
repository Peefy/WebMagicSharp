using System;
<<<<<<< HEAD
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Diagnostics;

using Newtonsoft.Json;

using WebMagicSharp.Model;
using WebMagicSharp.Pipelines;
using WebMagicSharp.Utils;


namespace WebMagicSharp.Pipelines
{
    public class FilePageModelPipeline : FilePersistentBase, IPageModelPipeline<object>
    {
        public FilePageModelPipeline()
        {
            this.path = "/data/webmagic";
        }

        public FilePageModelPipeline(String path)
        {
            this.path = path;
        }

        public virtual void Process(object t, ITask task)
        {
            var totalPath = this.path + PathSeparator + task.GetGuid() + PathSeparator;
            try
            {
                var fileName = "";
                if (t is IHasKey o)
                {
                    fileName = totalPath + o.Key + ".xml";
                }
                else
                {
                    fileName = totalPath + MD5.Create(t.ToString()) + ".xml";
                }
                var stream = new MemoryStream();
                var xs = new XmlSerializer(t.GetType());
                xs.Serialize(stream, t);
                var data = stream.ToArray();
                stream.Close();
                File.WriteAllText(fileName, Encoding.Default.GetString(data));
            }
            catch (IOException e)
            {
                Debug.WriteLine("write file error:" + e);
            }


        }
    }

=======
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Pipelines
{
    public class FilePageModelPipeline
    {
    }
>>>>>>> fe0493ec8c1dbdf2b0c98f6d6f050907a7aed103
}
