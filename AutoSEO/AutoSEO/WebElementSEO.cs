using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Threading;

namespace AutoSEO
{
    /// <summary>
    /// 浏览器驱动
    /// </summary>
    public enum DriverEnum
    {
        Chrome,
        Firefox
    }

    /// <summary>
    /// 浏览器类型
    /// </summary>
    public enum BrowserEnum
    {
        baidu,
        soguo,
        bing,
        B360
    }

    /// <summary>
    /// 
    /// </summary>
    public class WebElementSEO
    {
        public IWebDriver webDriver;
        /// <summary>
        /// 全局计数器
        /// </summary>
        public static int gcounter = 0;
        /// <summary>
        /// 单页面计数器
        /// </summary>
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

            //webDriver.Manage().Window.Maximize();
        }

        /// <summary>
        /// 更换浏览器
        /// </summary>
        /// <param name="driverEnum"></param>
        public void ChangeDriver(DriverEnum driverEnum)
        {
            string ip = RandomIP();
            switch (driverEnum)
            {
                case DriverEnum.Firefox:
                    {
                        //代理IP有待测试
                        var profile = new FirefoxOptions();
                        ////profile.AddArgument("proxy-server=202.20.16.82");
                        profile.AddArgument($"x-forwarded-for={ip}");
                        //profile.AddArgument($"network.proxy.http={ip}");
                        //profile.AddArgument("network.proxy.ssl=202.20.16.85");
                        webDriver = new FirefoxDriver(profile);
                    }
                    break;
                default:
                    {
                        var profile = new ChromeOptions();
                        profile.AddArgument($"x-forwarded-for={ip}");
                        //profile.AddArgument($"network.proxy.http={ip}");
                        webDriver = new ChromeDriver(profile);
                        break;
                    }
            }
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="word"></param>
        public void Jump(string word)
        {
            int random = rd.Next(15) % 8;
            switch (random)
            {
                case 0:
                    Jump(word, BrowserEnum.baidu, "https://www.hao123.com/", ".textInput.input-hook", ".g-cp.submitInput.button-hook");
                    break;
                case 1:
                    Jump(word, BrowserEnum.baidu, "https://home.firefoxchina.cn/", "#search-key", "#search-submit");
                    break;
                case 2:
                    Jump(word, BrowserEnum.baidu, "https://www.2345.com/", ".sch_inbox > input", "#j_search_sbm");
                    break;
                //case 3:
                //    Jump(word, BrowserEnum.B360, "https://hao.360.com/", "#search-kw", "#search-btn");
                //    break;
                //case 4:
                //    Jump(word, BrowserEnum.B360, "https://www.so.com/", "#input", "#search-button");
                //    break;
                //case 5:
                //    Jump(word, BrowserEnum.soguo, "https://www.sogou.com/", "#query", "#stb");
                //    break;
                //case 6:
                //    Jump(word, BrowserEnum.soguo, "https://123.sogou.com/", "#engineKeyWord", "#engineBtn");
                //    break;
                //case 7:
                //    Jump(word, BrowserEnum.bing, "https://cn.bing.com/", "#sb_form_q", "#sb_form_go");
                //    break;
                default:
                    Jump(word);
                    break;
            }
        }

        /// <summary>
        /// 百度跳转
        /// </summary>
        /// <param name="keyWord">关键字</param>
        public void Jump(string keyWord, BrowserEnum browserEnum = BrowserEnum.baidu, string www = "https://www.baidu.com/", string inputId = "#kw", string btnId = "#su")
        {
            try
            {
                //1.百度
                GoToUrl(www);
                var currentWindow = webDriver.CurrentWindowHandle;
                Sleep();

                //2.关键字数据
                foreach (var k in keyWord)
                {
                    SetValue(inputId, k.ToString());
                }
                Sleep();

                //3.搜索
                ClickByCss(btnId);
                Sleep();

                //页面跳转
                var allWindow = webDriver.WindowHandles;
                foreach (var str in allWindow)
                {
                    if (str != currentWindow)
                    {
                        webDriver.SwitchTo().Window(str);
                    }
                }

                //4.查询网址
                switch (browserEnum)
                {
                    case BrowserEnum.B360:
                        Target360Web();
                        break;
                    case BrowserEnum.bing:
                        TargetBingWeb();
                        break;
                    case BrowserEnum.soguo:
                        TargetSoWeb();
                        break;
                    default:
                        TargetBaiduWeb();
                        break;
                }

                //5.关闭浏览器
                Sleep(); ;//:TODO测试的时候查看效果，正式删除
                Close();
            }
            catch (NoSuchElementException ex)
            {
                logger.Warn($"元素没有发现：{ex.ToString()}");
                Close();
            }
            catch (NoSuchWindowException ex)
            {
                logger.Warn($"浏览器已经关闭：{ex.ToString()}");
                Close(); ;
            }
            catch (Exception ex)
            {
                logger.Error($"其他错误：{ex.ToString()}");
                Close();
            }
        }

        /// <summary>
        /// 目标网站跳转
        /// </summary>
        private void TargetBaiduWeb(string targetWebName = "www.guotaigold.hk")
        {
            //4.查询网址
            bool result = ClickByPartialText(targetWebName);
            while (!result)
            {
                ClickByXpath("//*[@id='page']/a[contains(@class,'n')][last()]");
                result = ClickByPartialText(targetWebName);
                if (counter >= 10)
                {
                    logger.Error($"运行10次还没有查询到，检查监控是否已经不能用：{webDriver.Url}");
                    break;//十次尝试依然不行退出
                }
            }
        }

        /// <summary>
        /// 搜狗目标网站跳转
        /// </summary>
        private void TargetSoWeb(string targetWebName = "www.guotaigold.hk")
        {
            //4.查询网址
            bool result = ClickByXpath($"//div[@id='main']//a[contains(@href,'{targetWebName}')]/../../h3/a");
            while (!result)
            {
                ClickByXpath("//*[@id='sogou_next']");
                result = ClickByXpath($"//div[@id='main']//a[contains(@href,'{targetWebName}')]/../../h3/a");
                if (counter >= 10)
                {
                    logger.Error($"运行10次还没有查询到，检查监控是否已经不能用：{webDriver.Url}");
                    break;//十次尝试依然不行退出
                }
            }
        }

        /// <summary>
        /// 360目标网址跳转
        /// </summary>
        /// <param name="targetWebName"></param>
        private void Target360Web(string targetWebName = "www.guotaigold.hk")
        {
            //4.查询网址
            bool result = ClickByXpath($"//div[@id='main']//a[contains(@href,'{targetWebName}')]/../../h3/a");
            while (!result)
            {
                ClickByXpath("//*[@id='snext']");
                result = ClickByXpath($"//div[@id='main']//a[contains(@href,'{targetWebName}')]/../../h3/a");
                if (counter >= 10)
                {
                    logger.Error($"运行10次还没有查询到，检查监控是否已经不能用：{webDriver.Url}");
                    break;//十次尝试依然不行退出
                }
            }
        }

        /// <summary>
        /// bing目标网站跳转
        /// </summary>
        private void TargetBingWeb(string targetWebName = "www.guotaigold.hk")
        {
            //4.查询网址
            bool result = ClickByXpath($"//a[contains(@href,'{targetWebName}')]");
            while (!result)
            {
                ClickByXpath($"//a[contains(@class,'sb_pagN')]");
                result = ClickByXpath($"//a[contains(@href,'{targetWebName}')]");
                if (counter >= 10)
                {
                    logger.Error($"运行10次还没有查询到，检查监控是否已经不能用：{webDriver.Url}");
                    break;//十次尝试依然不行退出
                }
            }
        }

        /// <summary>
        /// 随机IP地址
        /// </summary>
        /// <returns></returns>
        private string RandomIP()
        {
            string ip = "";
            int start = rd.Next(3) % 3;
            switch (start)
            {
                case 0:
                    ip = $"113.{rd.Next(7)}.{rd.Next(255)}.{rd.Next(255)}"; //113.0.0.0 - 113.7.255.255   联通
                    break;
                case 1:
                    ip = $"112.{rd.Next(63)}.{rd.Next(255)}.{rd.Next(255)}"; //112.0.0.0 - 112.63.255.255  移动
                    break;
                default:
                    ip = $"111.{rd.Next(63)}.{rd.Next(255)}.{rd.Next(255)}"; //111.0.0.0 - 111.63.255.255  移动
                    break;
            }

            return ip;
        }

        /// <summary>
        /// 关闭浏览器
        /// </summary>
        public void Close()
        {
            gcounter = 0;
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
        public void ClickByCss(string cssSelector)
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
                element.Click();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 根据xpath点击
        /// </summary>
        /// <param name="id"></param>
        public bool ClickByXpath(string path)
        {
            bool result = false;
            var element = FindElement(By.XPath(path));
            if (element != null)
            {
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
                logger.Warn($"元素没有发现:{by.ToString()}");
                gcounter++;
                counter++;
            }

            return element;
        }

        /// <summary>
        /// 随机休眠时间（单位：秒）
        /// </summary>
        private void Sleep()
        {
            Thread.Sleep(TimeSpan.FromSeconds(rd.Next(2, 10)));
        }

        /// <summary>
        /// 搜索关键词=>点击网站=>停留在网站上=>关闭浏览器
        /// </summary>
        public void Test()
        {
            string word = "国泰金业";
            //Jump(word, BrowserEnum.baidu, "https://www.hao123.com/", ".textInput.input-hook", ".g-cp.submitInput.button-hook");

            //Jump(word, BrowserEnum.baidu, "https://home.firefoxchina.cn/", "#search-key", "#search-submit");

            //Jump(word, BrowserEnum.baidu, "https://www.2345.com/", ".sch_inbox > input", "#j_search_sbm");

            //Jump(word, BrowserEnum.B360, "https://hao.360.com/", "#search-kw", "#search-btn");

            //Jump(word, BrowserEnum.B360, "https://www.so.com/", "#input", "#search-button");

            //Jump(word, BrowserEnum.soguo, "https://www.sogou.com/", "#query", "#stb");

            //Jump(word, BrowserEnum.soguo, "https://123.sogou.com/", "#engineKeyWord", "#engineBtn");

            //Jump(word, BrowserEnum.bing, "https://cn.bing.com/", "#sb_form_q", "#sb_form_go");

            Jump(word);
        }
    }
}
