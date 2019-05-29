using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoSEO
{
    /// <summary>
    /// 常量配置
    /// </summary>
    public static class AppConstant
    {
        public static Random rd = new Random();
        /// <summary>
        /// 关键词
        /// </summary>
        public static string[] words;
        /// <summary>
        /// 线程数
        /// </summary>
        public static int threadCount;
        /// <summary>
        /// 请求开始时间
        /// </summary>
        public static int startRequestTime;
        /// <summary>
        /// 请求结束时间
        /// </summary>
        public static int endRequestTime;

        /// <summary>
        /// IP范围
        /// </summary>
        public static int[,] rangIP = new int[3, 2] {
                { 1895825408, 1896349695 },  //113.0.0.0 - 113.7.255.255   联通  113*256^3 至 113*256^3 + 7*256^2 + 255*256 + 255
                { 1879048192, 1883242495 },  //112.0.0.0 - 112.63.255.255  移动  112*256^3 至 112*256^3 + 63*256^2 + 255*256 + 255
                { 1862270976, 1866465279 }   //111.0.0.0 - 111.63.255.255  移动 ,111*256^3 至 111*256^3 + 63*256^2 + 255*256 + 255 第一位超过2^8-1=127的用负数
        };

        /// <summary>
        /// 初始化数据
        /// </summary>
        public static void Init()
        {
            FetchData();
            WatcherFile();
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        public static void FetchData()
        {
            try
            {
                ConfigurationManager.RefreshSection("appSettings");
                words = ConfigurationManager.AppSettings["words"].Split(',');
                threadCount = int.Parse(ConfigurationManager.AppSettings["threadCount"]);
                IEnumerable<int> requestTime = ConfigurationManager.AppSettings["threadCount"].Split(',').Select(x => int.Parse(x));
                startRequestTime = requestTime.First();
                endRequestTime = requestTime.Last();
            }
            catch(Exception ex)
            {
                words = new string[] { "国泰金业" };
                threadCount = 1;
                startRequestTime = 5;
                endRequestTime = 10;
            }
        }

        /// <summary>
        /// 监听文件变化
        /// </summary>
        private static void WatcherFile()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = Environment.CurrentDirectory;
            watcher.Filter = "*.config";

            watcher.Changed += (object sender, FileSystemEventArgs e) => {
                Thread.Sleep(100);
                FetchData();
            };
            watcher.EnableRaisingEvents = true;
        }
    }
}
