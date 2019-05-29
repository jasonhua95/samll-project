using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
            logger.Info($"程序开始启动");
            AppConstant.Init();
            //string[] words = ConfigurationManager.AppSettings["words"].Split(',');
            List<Task> taskList = new List<Task>();
            while (true)
            {

                foreach (var word in AppConstant.words)
                {
                    try
                    {
                        var t = Task.Run(() =>
                        {
                            Thread.Sleep(20);
                            logger.Info($"百度搜索的关键字：{word}");
                            int random = AppConstant.rd.Next(15) % 3;
                            DriverEnum driver = random == 0 ? DriverEnum.Firefox : DriverEnum.Chrome;
                            WebElementSEO utils = new WebElementSEO(driver, "www.guotaigold.hk");
                            //MWebElementSEO utils = new MWebElementSEO(driver, "m.guotaigold.hk");
                            utils.Jump(word);
                        });
                        taskList.Add(t);

                        if (taskList.Count() >= AppConstant.threadCount)
                        {
                            Task.WaitAll(taskList.ToArray());
                            taskList.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error($"Main中程序运行错误：{ex.ToString()}");
                        taskList.Clear();
                    }
                    count++;
                }

                if (count % 10 == 0) logger.Info($"程序运行的此时：{count}");

                Thread.Sleep(TimeSpan.FromSeconds(AppConstant.rd.Next(5, 10)));
            }

        }

    }
}
