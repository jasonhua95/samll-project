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
                        random = rd.Next(15) % 3;
                        switch (random)
                        {
                            case 0:
                                utils.BaiduJump(word, "https://www.hao123.com/", ".textInput.input-hook", ".g-cp.submitInput.button-hook");
                                break;
                            case 1:
                                utils.BaiduJump(word, "https://home.firefoxchina.cn/", "#search-key", "#search-submit");
                                break;
                            default:
                                utils.BaiduJump(word);
                                break;
                        }

                    });

                    tasks.Add(task);
                    if (tasks.Count > 5)
                    {
                        Task.WaitAll(tasks.ToArray());
                        tasks.Clear();
                    }
                }

                Task.WaitAll(tasks.ToArray());
                if (WebElementSEO.counter >= 10)
                {
                    logger.Error($"程序运行10次，依然不能正常访问，查看是否不起作用！这里将来发送邮件提示");
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
