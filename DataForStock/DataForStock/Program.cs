using DataForStock.SchedulerService;
using FluentScheduler;
using NLog;
using System;

namespace DataForStock
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();

            JobManager.Initialize(new StockRegistry());

            logger.Info("测试下");
            Console.WriteLine("Hello World!");

            Console.Read();
        }
    }
}
