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
            logger.Info($"程序开始启动");

            string[] words = ConfigurationManager.AppSettings["words"].Split(',');
            Random rd = new Random();
            while (true)
            {

                foreach (var word in words)
                {
                    try
                    {
                        int random = rd.Next(15) % 3;
                        DriverEnum driver = random == 0 ? DriverEnum.Firefox : DriverEnum.Chrome;
                        WebElementSEO utils = new WebElementSEO(driver, "m.guotaigold.hk");
                        //MWebElementSEO utils = new MWebElementSEO(driver, "m.guotaigold.hk");
                        utils.Jump(word);
                    }
                    catch (Exception ex)
                    {
                        logger.Error($"Main中程序运行错误：{ex.ToString()}");
                    }
                    count++;
                }

                if (count % 10 == 0) logger.Info($"程序运行的此时：{count}");

                Thread.Sleep(TimeSpan.FromMinutes(rd.Next(5, 10)));
            }

        }
    }
}
