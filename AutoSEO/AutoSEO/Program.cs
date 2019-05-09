using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace AutoSEO
{
    class Program
    {
        static int count = 0;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            string[] words = ConfigurationManager.AppSettings["words"].Split(',');
            int length = words.Length - 1;
            Random rd = new Random();
            while (true)
            {

                List<Task> tasks = new List<Task>();
                for (int i = 0; i <= length; i++)
                {
                    string word = words[rd.Next(length)];
                    var task = Task.Run(() =>
                    {
                        try
                        {
                            int random = rd.Next(15) % 3;
                            DriverEnum driver = random == 0 ? DriverEnum.Firefox : DriverEnum.Chrome;
                            WebElementSEO utils = new WebElementSEO(driver);
                            utils.Jump(word);
                        }
                        catch (Exception ex)
                        {
                            logger.Error($"Main中程序运行错误：{ex.ToString()}");
                        }
                    });

                    tasks.Add(task);
                    if (tasks.Count >= 5)
                    {
                        logger.Info($"运行次数：{count}");
                        Task.WaitAll(tasks.ToArray());
                        tasks.Clear();
                    }
                    count++;
                }

                Task.WaitAll(tasks.ToArray());
                if (WebElementSEO.gcounter >= 20)
                {
                    logger.Error($"程序连续运行20次，依然不能正常访问，查看是否不起作用！这里将来发送邮件提示");
                    break;
                }
                Thread.Sleep(TimeSpan.FromMinutes(rd.Next(10, 20)));
            }

            //WebElementSEO utils = new WebElementSEO(DriverEnum.Chrome);
            //utils.Test();

            Console.WriteLine("运行结束！");
        }
    }
}
