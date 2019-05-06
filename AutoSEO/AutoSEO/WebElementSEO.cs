using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AutoSEO
{
    public enum DriverEnum
    {
        Chrome,
        Firefox,
        InternetExplorer
    }

    /// <summary>
    /// 
    /// </summary>
    public class WebElementSEO
    {
        public IWebDriver webDriver;
        public int counter = 0;
        private Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 休眠时间
        /// </summary>
        private Random rd = new Random();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="driverEnum"></param>
        public WebElementSEO(DriverEnum driverEnum)
        {
            ChangeDriver(driverEnum);
            //设置全局操作等待最长20秒
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            //删除所有cookie
            webDriver.Manage().Cookies.DeleteAllCookies();
            //window.navigator.webdriver  正常浏览器这个值是false

            webDriver.Manage().Window.Maximize();
        }

        /// <summary>
        /// 搜索关键词=>点击网站=>停留在网站上=>关闭浏览器
        /// </summary>
        public void Test()
        {
            Console.WriteLine("运行开始！");
            GoToUrl("https://www.baidu.com/");
            SetValue(".s_ipt", "测试");
            Sleep();
            Click(".bg.s_btn");
            Sleep();
            webDriver.FindElement(By.LinkText("下一页>")).Click();
            Sleep();
            webDriver.FindElement(By.PartialLinkText("www.apesk.com")).Click();
            Sleep();
            Close();
        }

        /// <summary>
        /// 百度跳转
        /// </summary>
        /// <param name="keyWord">关键字</param>
        public void BaiduJump(string keyWord, string www = "https://www.baidu.com/", string inputId = "#kw", string btnId = "#su")
        {
            try
            {
                Random rd = new Random();
                //1.百度
                GoToUrl(www);
                var currentWindow = webDriver.CurrentWindowHandle;
                Sleep();

                //2.关键字数据
                foreach (var k in keyWord)
                {
                    //Thread.Sleep(TimeSpan.FromMilliseconds(rd.Next(10)));
                    SetValue(inputId, k.ToString());
                }
                Sleep();

                //3.搜索
                Click(btnId);
                Sleep();

                //页面跳转，新打开页面
                var allWindow = webDriver.WindowHandles;
                foreach (var str in allWindow)
                {
                    if (str != currentWindow)
                    {
                        webDriver.SwitchTo().Window(str);
                    }
                }

                //4.查询网址
                bool result = ClickByPartialText("www.apesk.com/");
                while (!result)
                {
                    ClickByText("下一页>");
                    result = ClickByPartialText("www.apesk.com/");
                    if (counter >= 10)
                    {
                        logger.Error($"运行10次还没有查询到，检查监控是否已经不能用");
                        break;//十次尝试依然不行退出
                    }
                }

                //5.关闭浏览器
                Sleep(); ;//:TODO测试的时候查看效果，正式删除
                Close();
            }
            catch (NoSuchElementException ex)
            {
                logger.Warn($"元素没有发现：{ex.ToString()}");
            }
            catch (NoSuchWindowException ex)
            {
                logger.Warn($"浏览器已经关闭：{ex.ToString()}");
            }
            catch (Exception ex)
            {
                logger.Error($"其他错误：{ex.ToString()}");
            }
        }

        /// <summary>
        /// 更换浏览器
        /// </summary>
        /// <param name="driverEnum"></param>
        public void ChangeDriver(DriverEnum driverEnum)
        {
            switch (driverEnum)
            {
                case DriverEnum.Firefox:
                    webDriver = new FirefoxDriver();

                    //var profile = new FirefoxOptions();
                    //profile.SetPreference("X-Forwarded-For", "113.89.96.200");//X-Real-IP,HTTP_X_FORWARDED_FOR,Accept-Language
                    //profile.SetPreference("Accept-Language", "113.89.96.200");//X-Real-IP,HTTP_X_FORWARDED_FOR,Accept-Language

                    //webDriver = new FirefoxDriver(profile);
                    break;
                //case DriverEnum.InternetExplorer: //IE需要管理员身份运行，IE元素单击没反应
                //    webDriver = new InternetExplorerDriver();
                //    break;
                default:
                    webDriver = new ChromeDriver();
                    break;
            }
        }

        /// <summary>
        /// 关闭浏览器
        /// </summary>
        public void Close()
        {
            counter = 0;
            webDriver.Close();
            webDriver.Quit();
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        /// <summary>
        /// 打开网址
        /// </summary>
        /// <param name="url">网址地址</param>
        public void GoToUrl(string url)
        {
            webDriver.Url = url;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="cssSelector">id</param>
        /// <param name="value">value</param>
        public void SetValue(string cssSelector, string value)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            if (element != null) element.SendKeys(value);
        }

        /// <summary>
        /// 单击
        /// </summary>
        /// <param name="cssSelector"></param>
        public void Click(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            if (element != null) element.Click();
        }

        /// <summary>
        /// 单击
        /// </summary>
        /// <param name="id"></param>
        public bool ClickByText(string text)
        {
            bool result = false;
            var element = FindElement(By.LinkText(text));
            if (element != null)
            {
                element.Click();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 单击
        /// </summary>
        /// <param name="id"></param>
        public bool ClickByPartialText(string text)
        {
            bool result = false;
            var element = FindElement(By.PartialLinkText(text));
            if (element != null)
            {
                //Actions actions = new Actions(webDriver);
                //actions.MoveToElement(element).Click().Perform();
                element.Click();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 发现元素
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        private IWebElement FindElement(By by)
        {
            IWebElement element = null;
            try
            {
                element = webDriver.FindElement(by);
            }
            catch (NoSuchElementException ex)
            {
                logger.Warn($"元素没有发现：{ex.ToString()}");
                counter++;
            }

            return element;
        }

        /// <summary>
        /// 随机休眠时间
        /// </summary>
        private void Sleep()
        {
            Thread.Sleep(TimeSpan.FromSeconds(rd.Next(10)));
        }
    }
}
