using Newtonsoft.Json;
using NLog;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

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
    /// PC刷新关键字
    /// </summary>
    public class SeleniumUtils
    {
        /// <summary>
        /// 单页面错误计数器
        /// </summary>
        private int counter = 0;

        /// <summary>
        /// 日志
        /// </summary>
        private Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 已经操作过的窗口
        /// </summary>
        private List<string> oldWindowHandles = new List<string>();

        public IWebDriver webDriver;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="driverEnum"></param>
        public SeleniumUtils(IWebDriver remoteWebDriver)
        {
            webDriver = remoteWebDriver;
            //设置全局操作等待最长60秒
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            //删除所有cookie
            //webDriver.Manage().Cookies.DeleteAllCookies();
            //window.navigator.webdriver  正常浏览器这个值是false

            //webDriver.Manage().Window.Maximize();
        }

        /// <summary>
        /// 页面跳转
        /// </summary>
        public void SkipPage()
        {
            var allWindow = webDriver.WindowHandles;
            foreach (var str in allWindow)
            {
                if (!oldWindowHandles.Contains(str))
                {
                    webDriver.SwitchTo().Window(str);
                }
            }
            oldWindowHandles.Add(webDriver.CurrentWindowHandle);
        }

        /// <summary>
        /// 打开网址
        /// </summary>
        /// <param name="url">网址地址</param>
        public void GoToUrl(string url)
        {
            webDriver.Url = url;
            oldWindowHandles.Add(webDriver.CurrentWindowHandle);
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="cssSelector">css选择器</param>
        /// <param name="value">值</param>
        public bool SetValue(string cssSelector, string value)
        {
            bool result = false;
            var element = FindElement(By.CssSelector(cssSelector));
            if (element != null)
            {
                element.SendKeys(value);
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 单击
        /// </summary>
        /// <param name="cssSelector"></param>
        public bool ClickByCss(string cssSelector)
        {
            bool result = false;
            var element = FindElement(By.CssSelector(cssSelector));
            if (element != null)
            {
                logger.Info($"点击的文本：{element.Text}，href:{element.GetAttribute("href")}");
                element.Click();
                result = true;
            }
            return result;
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
                logger.Info($"点击的文本：{element.Text}，href:{element.GetAttribute("href")}");
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
                logger.Info($"点击的文本：{element.Text}，href:{element.GetAttribute("href")}");
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
                logger.Info($"点击的文本：{element.Text}，href:{element.GetAttribute("href")}");
                element.Click();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 根据xpath点击对隐藏的
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isLoopSearch">是否需要循环操作</param>
        public bool ClickByXpathForDisplayed(string path)
        {
            bool result = false;
            var element = FindElement(By.XPath(path));
            if (element != null && element.Displayed)
            {
                logger.Info($"点击的文本：{element.Text}，href:{element.GetAttribute("href")}");
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
                counter++;
            }
            catch (Exception ex)
            {
                logger.Warn($"其他异常错误:{by.ToString()}");
                counter++;
            }

            return element;
        }

        /// <summary>
        /// 关闭浏览器
        /// </summary>
        public void Close()
        {
            counter = 0;
            webDriver.Close();
            webDriver.Quit();
        }

    }
}
