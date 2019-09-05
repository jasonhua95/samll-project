using DataForStock.FetchData;
using FluentScheduler;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataForStock.SchedulerService
{
    /// <summary>
    /// 定时任务
    /// </summary>
    public class StockRegistry : Registry
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        public StockRegistry() {
            // 每天执行一次（这里是在每天的下午 15:40 分执行），可以不用类，直接虚拟方法
            Schedule(() =>
            {
                logger.Info($"定时任务开始运行");
                //Task t = FetchDataService.FetchDataAsync();
                //Task.WhenAny(t);
                FetchDataService.Test2();
                logger.Info($"定时任务结束运行");
            }
            ).ToRunNow().AndEvery(1).Minutes();
        }
    }
}
