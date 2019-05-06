using NLog;
using System;
using System.Collections.Generic;
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
            Random rd = new Random();
            while (true)
            {
                int random = rd.Next(15) % 3;
                DriverEnum driver = random == 0 ? DriverEnum.Firefox : DriverEnum.Chrome;
                WebElementSEO utils = new WebElementSEO(DriverEnum.Firefox);
                utils.BaiduJump("测试");
                if (utils.counter >= 10)
                {
                    logger.Error($"程序运行10次，依然不能正常访问，查看是否不起作用！这里将来发送邮件提示");
                    break;
                }
                Thread.Sleep(TimeSpan.FromSeconds(rd.Next(10)));
            }
            Console.WriteLine("运行结束！");
        }
    }
}
