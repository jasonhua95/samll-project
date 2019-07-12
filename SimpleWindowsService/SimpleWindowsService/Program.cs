using FluentScheduler;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Topshelf;

namespace SimpleWindowsService
{
    /// <summary>
    /// cmd控制台管理员启动，找到exe地址，以下面程序运行下面语句，在服务中可以看到执行的情况
    /// 安装：SimpleWindowsService.exe install
    /// 启动：SimpleWindowsService.exe start
    /// 卸载：SimpleWindowsService.exe uninstall
    /// </summary>
    class Program
    {
        private static readonly Logger logger = LogManager.GetLogger("Program");
        static void Main(string[] args)
        {
            var rc = HostFactory.Run(x =>                         //1.启动程序
            {
                logger.Info($"主程序{DateTime.Now}");
                x.Service<MyJob>(s =>                         //2.设置服务类型
                {
                    s.ConstructUsing(name => new MyJob());    //3.创建服务实例
                    s.WhenStarted(tc => tc.Start());              //4.启动程序
                    s.WhenStopped(tc => tc.Stop());               //5.停止程序
                });
                x.RunAsLocalSystem();                             //6.本地系统运行

                x.SetDescription("超级简单的windows服务");         //7.windows服务的描述
                x.SetDisplayName("SimpleWindowsService 服务");                        //8.windows服务的显示名称
                x.SetServiceName("SimpleWindowsService");                        //9.windows服务的服务名称
            });
            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());  //11.退出程序
            Environment.ExitCode = exitCode;

        }
    }

    public class TownCrier
    {
        private static readonly Logger logger = LogManager.GetLogger("logTest");
        readonly Timer _timer;
        public TownCrier()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) =>
            {
                Console.WriteLine($"It is {DateTime.Now} and all is well");
                logger.Info($"It is {DateTime.Now} and all is well");
            };
        }
        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }
    }


    /// <summary>
    /// 用MySchedule的任务定时功能
    /// </summary>
    public class MyJob {
        private static readonly Logger logger = LogManager.GetLogger("MyJob");
        public MyJob() { }
        public void Start()
        {
            logger.Info($"MySchedule启动 {DateTime.Now}");
            JobManager.Initialize(new MySchedule());
        }
        public void Stop()
        {
            logger.Info($"MySchedule停止 {DateTime.Now}");
            JobManager.Stop();
        }
    }

    /// <summary>
    /// 定时器
    /// </summary>
    public class MySchedule : Registry
    {
        private static readonly Logger logger = LogManager.GetLogger("MySchedule");
        public MySchedule()
        {
            SetNewsSchedule();
        }

        /// <summary>
		/// 设置任务
		/// </summary>
		private void SetNewsSchedule()
        {
            //获取链接发送邮件
            Schedule(() =>
            {
                logger.Info($"MySchedule运行 {DateTime.Now}");
            }
            ).ToRunNow().AndEvery(100).Milliseconds();
        }
    }
}
