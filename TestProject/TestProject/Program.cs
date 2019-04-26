namespace TestProject
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using log4net;
    using log4net.Config;

    internal class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static async Task Main(string[] args)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetExecutingAssembly());
            var processName = Assembly.GetEntryAssembly().GetName().Name;
            XmlConfigurator.Configure(logRepository, new FileInfo(@"log4net.config"));
            GlobalContext.Properties["pname"] = processName;
            GlobalContext.Properties["pid"] = Process.GetCurrentProcess().Id;

            Console.OutputEncoding = Encoding.UTF8;
            var exitCode = 0;

            Logger.Info($"{processName} started!");
            var stopWatch = Stopwatch.StartNew();
            try
            {
                await TestProject.Main.Work(args);
            }
            catch (AggregateException ae)
            {
                exitCode = -1;
                Logger.Error("One or more exceptions occurred:");

                foreach (var exception in ae.Flatten().InnerExceptions)
                {
                    Logger.Error(exception.ToString());
                }
            }
            catch (Exception ex)
            {
                exitCode = -1;
                Logger.Error($"Exception occurred: {ex}");
            }
            finally
            {
                stopWatch.Stop();
            }

            Logger.Info($"{processName} finished! Time taken: {(double)stopWatch.ElapsedMilliseconds / 1000:0.000} secs");
            Environment.ExitCode = exitCode;
        }
    }
}
