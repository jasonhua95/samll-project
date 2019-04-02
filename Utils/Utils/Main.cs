namespace Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using log4net;
    using Vanilla;

    internal class Main
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal static async Task Work(string[] args)
        {
            await Task.FromResult(0);
            //Logger.Info("Hello huayingjie! CLR:4.0.30319.42000");

            //Console.WriteLine(await ReplaceRenderer.ParseAsync("hi @UserName,年龄： @Age，小名：@XiaoName 。", new { UserName = "测试", Age = 19, XiaoName = "小明" }));
        }
    }
}
