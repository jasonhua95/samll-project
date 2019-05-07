using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoSEO
{
    class Program
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            string[] words = ConfigurationManager.AppSettings["words"].Split(',');
            Random rd = new Random();
            while (true)
            {

                List<Task> tasks = new List<Task>();
                foreach (var word in words)
                {
                    var task = Task.Run(() =>
                    {
                        int random = rd.Next(15) % 3;
                        DriverEnum driver = random == 0 ? DriverEnum.Firefox : DriverEnum.Chrome;
                        WebElementSEO utils = new WebElementSEO(driver);
                        random = rd.Next(15) % 5;
                        switch (random)
                        {
                            case 0:
                                utils.Jump(word, BrowserEnum.baidu, "https://www.hao123.com/", ".textInput.input-hook", ".g-cp.submitInput.button-hook");
                                break;
                            case 1:
                                utils.Jump(word, BrowserEnum.baidu, "https://home.firefoxchina.cn/", "#search-key", "#search-submit");
                                break;
                            case 2:
                                utils.Jump(word, BrowserEnum.baidu, "https://www.2345.com/", ".sch_inbox > input", "#j_search_sbm");
                                break;
                            //case 3:
                            //    utils.Jump(word, BrowserEnum.B360, "https://hao.360.com/", "#search-kw", "#search-btn");
                            //    break;
                            //case 4:
                            //    utils.Jump(word, BrowserEnum.B360, "https://www.so.com/", "#input", "#search-button");
                            //    break;
                            //case 5:
                            //    utils.Jump(word, BrowserEnum.soguo, "https://www.sogou.com/", "#query", "#stb");
                            //    break;
                            //case 6:
                            //    utils.Jump(word, BrowserEnum.soguo, "https://123.sogou.com/", "#engineKeyWord", "#engineBtn");
                            //    break;

                            default:
                                utils.Jump(word);
                                break;
                        }
                    });

                    tasks.Add(task);
                    if (tasks.Count >= 5)
                    {
                        Task.WaitAll(tasks.ToArray());
                        tasks.Clear();
                    }
                }

                Task.WaitAll(tasks.ToArray());
                if (WebElementSEO.gcounter >= 20)
                {
                    logger.Error($"程序连续运行20次，依然不能正常访问，查看是否不起作用！这里将来发送邮件提示");
                    break;
                }
                Thread.Sleep(TimeSpan.FromSeconds(rd.Next(10)));
            }

            //WebElementSEO utils = new WebElementSEO(DriverEnum.Firefox);
            //utils.Test();

            Console.WriteLine("运行结束！");
        }
    }
}
