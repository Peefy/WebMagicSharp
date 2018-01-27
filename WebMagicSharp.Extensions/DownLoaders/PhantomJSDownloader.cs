using System;
using System.Text;
using System.IO;
using System.Diagnostics;

using WebMagicSharp.DownLoaders;
using WebMagicSharp.Selector;

namespace WebMagicSharp.DownLoaders
{
    public class PhantomJSDownloader : AbstractDownloader
    {
   
    private static string crawlJsPath;
    private static string phantomJsCommand = "phantomjs"; // default

    private int retryNum;
    private int threadNum;

    public PhantomJSDownloader()
    {
        this.InitPhantomjsCrawlPath();
    }

    /**
     * 添加新的构造函数，支持phantomjs自定义命令
     * 
     * example: 
     *    phantomjs.exe 支持windows环境
     *    phantomjs --ignore-ssl-errors=yes 忽略抓取地址是https时的一些错误
     *    /usr/local/bin/phantomjs 命令的绝对路径，避免因系统环境变量引起的IOException
     *   
     * @param phantomJsCommand phantomJsCommand
     */
    public PhantomJSDownloader(string phantomJsCommand)
    {
        this.InitPhantomjsCrawlPath();
        PhantomJSDownloader.phantomJsCommand = phantomJsCommand;
    }

    /**
     * 新增构造函数，支持crawl.js路径自定义，因为当其他项目依赖此jar包时，runtime.exec()执行phantomjs命令时无使用法jar包中的crawl.js
     * <pre>
     * crawl.js start --
     * 
     *   var system = require('system');
     *   var url = system.args[1];
     *   
     *   var page = require('webpage').create();
     *   page.settings.loadImages = false;
     *   page.settings.resourceTimeout = 5000;
     *   
     *   page.open(url, function (status) {
     *       if (status != 'success') {
     *           console.log("HTTP request failed!");
     *       } else {
     *           console.log(page.content);
     *       }
     *   
     *       page.close();
     *       phantom.exit();
     *   });
     *   
     * -- crawl.js end
     * </pre>
     * 具体项目时可以将以上js代码复制下来使用
     *   
     * example:
     *    new PhantomJSDownloader("/your/path/phantomjs", "/your/path/crawl.js");
     * 
     * @param phantomJsCommand phantomJsCommand
     * @param crawlJsPath crawlJsPath
     */
    public PhantomJSDownloader(String phantomJsCommand, String crawlJsPath)
    {
        PhantomJSDownloader.phantomJsCommand = phantomJsCommand;
        PhantomJSDownloader.crawlJsPath = crawlJsPath;
    }

    private void InitPhantomjsCrawlPath()
    {
        crawlJsPath = Path.Combine(Environment.CurrentDirectory, "crawl.js ");
    }

    public override Page Download(Request request, ITask task)
    {
            Debug.WriteLine("downloading page: " + request.GetUrl());
            var content = GetPage(request);
        if (content.Contains("HTTP request failed"))
        {
            for (int i = 1; i <= RetryNum; i++)
            {
                content = GetPage(request);
                if (!content.Contains("HTTP request failed"))
                {
                    break;
                }
            }
            if (content.Contains("HTTP request failed"))
            {
                //when failed
                var pageTemp = new Page();
                    pageTemp.SetRequest(request);
                return pageTemp;
            }
        }

        var page = new Page();
        page.SetRawText(content);
        page.SetUrl(new PlainText(request.GetUrl()));
        page.SetRequest(request);
        page.SetStatusCode(200);
        return page;
    }

    public override void SetThread(int threadNum)
    {
        this.threadNum = threadNum;
    }

        /// <summary>  
        /// 执行DOS命令，返回DOS命令的输出  
        /// </summary>  
        /// <param name="dosCommand">dos命令</param>  
        /// <param name="milliseconds">等待命令执行的时间（单位：毫秒），  
        /// 如果设定为0，则无限等待</param>  
        /// <returns>返回DOS命令的输出</returns>  
        public string Execute(string command, int seconds = 10)
        {
            string output = ""; //输出字符串  
            if (string.IsNullOrEmpty(command) == false)
            {
                Process process = new Process();//创建进程对象  
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",//设定需要执行的命令  
                                         //startInfo.Arguments = "/C " + command;//“/C”表示执行完命令后马上退出 
                                         //startInfo.Arguments = command;//“/C”表示执行完命令后马上退出 
                    UseShellExecute = false,//不使用系统外壳程序启动  
                    RedirectStandardInput = true,  //重定向输入  
                    RedirectStandardOutput = true, //重定向输出  
                    CreateNoWindow = true//不创建窗口  
                };
                process.StartInfo = startInfo;
                try
                {
                    if (process.Start())//开始进程  
                    {
                        if (seconds == 0)
                        {
                            process.WaitForExit();//这里无限等待进程结束  
                        }
                        else
                        {
                            process.WaitForExit(seconds); //等待进程结束，等待时间为指定的毫秒  
                        }
                        StreamWriter sw = process.StandardInput;
                        sw.WriteLine(command);
                        sw.Close();
                        //process.StandardInput.Close();
                        output = process.StandardOutput.ReadToEnd();//读取进程的输出  
                    }
                }
                catch
                {
                }
                finally
                {
                    if (process != null)
                        process.Close();
                }
            }
            return output;
        }

        protected String GetPage(Request request)
        {
            try
            {
                var url = request.GetUrl();
                return Execute(phantomJsCommand + " " + crawlJsPath + " " + url);
            }
            catch (IOException e)
            {
                Debug.WriteLine(e.Message);
            }

            return null;
        }

        public int RetryNum => retryNum;

        public PhantomJSDownloader SetRetryNum(int retryNum)
        {
            this.retryNum = retryNum;
            return this;
        }
    }
}