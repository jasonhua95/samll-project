using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AutoSEO
{
    /// <summary>
    /// PC刷新关键字
    /// </summary>
    public class WebElementSEO
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 休眠时间
        /// </summary>
        private Random rd = new Random();
        /// <summary>
        /// 单页面错误计数器
        /// </summary>
        private int counter = 0;
        /// <summary>
        /// 最大循环次数
        /// </summary>
        private int maxCounter = 10;

        private SeleniumUtils seleniumUtils;

        private string targetWebName;// "m.guotaigold.hk"; //www.guotaigold.hk

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="driverEnum"></param>
        public WebElementSEO(DriverEnum driverEnum, string targetWebName)
        {
            this.targetWebName = targetWebName;
            IWebDriver webDriver;
            string ip = RandomIP();
            switch (driverEnum)
            {
                case DriverEnum.Firefox:
                    {
                        ////代理IP有待测试
                        var option = new FirefoxOptions();
                        //////profile.AddArgument("proxy-server=202.20.16.82");
                        option.AddArgument($"x-forwarded-for={ip}");
                        //////profile.AddArgument($"network.proxy.http={ip}");
                        //////profile.AddArgument("network.proxy.ssl=202.20.16.85");
                        ////设置user agent为iphone6plus

                        //profile.AddArgument("--user-agent=iphone 6 plus");

                        webDriver = new FirefoxDriver(option);
                    }
                    break;
                default:
                    {
                        var option = new ChromeOptions();
                        option.AddArgument($"x-forwarded-for={ip}");
                        webDriver = new ChromeDriver(option);
                        break;
                    }
            }
            seleniumUtils = new SeleniumUtils(webDriver);
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
                //case 2:
                //    Jump(word, BrowserEnum.baidu, "https://www.2345.com/", ".sch_inbox > input", "#j_search_sbm");
                //    break;
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
                    Jump(word, BrowserEnum.baidu);
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
                seleniumUtils.GoToUrl(www);
                Thread.Sleep(TimeSpan.FromSeconds(rd.Next(3, 10)));

                //2.关键字数据
                foreach (var k in keyWord)
                {
                    seleniumUtils.SetValue(inputId, k.ToString());
                }
                Thread.Sleep(TimeSpan.FromSeconds(rd.Next(1, 3)));

                //3.搜索
                seleniumUtils.ClickByCss(btnId);
                Thread.Sleep(TimeSpan.FromSeconds(rd.Next(3, 5)));

                //4.查询网址
                seleniumUtils.SkipPage();
                switch (browserEnum)
                {
                    case BrowserEnum.B360:
                        counter = 0;
                        Target360Web();
                        break;
                    case BrowserEnum.bing:
                        counter = 0;
                        TargetBingWeb();
                        break;
                    case BrowserEnum.soguo:
                        counter = 0;
                        TargetSoWeb();
                        break;
                    default:
                        counter = 0;
                        TargetBaiduWeb();
                        break;
                }
                Thread.Sleep(TimeSpan.FromSeconds(rd.Next(5, 15)));

                //5.操作目标网址
                RandomClickMGT();

                //6.目标网址停留5-10分钟，关闭
                //Thread.Sleep(TimeSpan.FromMinutes(rd.Next(5, 10)));
                seleniumUtils.Close();
            }
            catch (NoSuchElementException ex)
            {
                logger.Warn($"元素没有发现：{ex.ToString()}");
                seleniumUtils.Close();
            }
            catch (NoSuchWindowException ex)
            {
                logger.Warn($"浏览器已经关闭：{ex.ToString()}");
                seleniumUtils.Close(); ;
            }
            catch (Exception ex)
            {
                logger.Error($"其他错误：{ex.ToString()}");
                seleniumUtils.Close();
            }
        }

        /// <summary>
        /// 目标网站跳转
        /// </summary>
        private bool TargetBaiduWeb()
        {
            //4.查询网址
            bool result = seleniumUtils.ClickByPartialText(targetWebName);
            if (!result)
            {
                bool next = seleniumUtils.ClickByXpath("//*[@id='page']/a[contains(@class,'n')][last()]");

                if (!next)
                {
                    logger.Warn($"百度下一页可能已经不起作用：//*[@id='page']/a[contains(@class,'n')][last()]");
                    counter++;
                }
                else
                {
                    counter++;
                    Thread.Sleep(10);
                    if (counter <= maxCounter) return TargetBaiduWeb();
                }
            }
            return result;
        }

        /// <summary>
        /// 搜狗目标网站跳转
        /// </summary>
        private bool TargetSoWeb()
        {
            //4.查询网址
            bool result = seleniumUtils.ClickByXpath($"//div[@id='main']//a[contains(@href,'{targetWebName}')]/../../h3/a");
            if (!result)
            {
                bool next = seleniumUtils.ClickByXpath("//*[@id='sogou_next']");

                if (!next)
                {
                    logger.Warn($"搜狗下一页可能已经不起作用：//*[@id='sogou_next']");
                }
                else
                {
                    counter++;
                    Thread.Sleep(10);
                    if (counter <= maxCounter) return TargetSoWeb();
                }
            }
            return result;
        }

        /// <summary>
        /// 360目标网址跳转
        /// </summary>
        /// <param name="targetWebName"></param>
        private bool Target360Web()
        {
            //4.查询网址
            bool result = seleniumUtils.ClickByXpath($"//div[@id='main']//a[contains(@href,'{targetWebName}')]/../../h3/a");
            if (!result)
            {
                bool next = seleniumUtils.ClickByXpath("//*[@id='snext']");

                if (!next)
                {
                    logger.Warn($"360下一页可能已经不起作用：//*[@id='snext']");
                }
                else
                {
                    counter++;
                    Thread.Sleep(10);
                    if (counter <= maxCounter) return Target360Web();
                }
            }
            return result;
        }

        /// <summary>
        /// bing目标网站跳转
        /// </summary>
        private bool TargetBingWeb()
        {
            //4.查询网址
            bool result = seleniumUtils.ClickByXpath($"//a[contains(@href,'{targetWebName}')]");
            if (!result)
            {
                bool next = seleniumUtils.ClickByXpath($"//a[contains(@class,'sb_pagN')]");

                if (!next)
                {
                    logger.Warn($"bing下一页可能已经不起作用：//a[contains(@class,'sb_pagN')]");
                }
                else
                {
                    counter++;
                    Thread.Sleep(10);
                    if (counter <= maxCounter) return Target360Web();
                }
            }
            return result;
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

        /// <summary>
        /// 官网跳转
        /// </summary>
        private void RandomClickGT()
        {
            bool result = false;

            seleniumUtils.SkipPage();
            Thread.Sleep(TimeSpan.FromSeconds(rd.Next(3, 10)));

            //1.关闭弹框
            seleniumUtils.ClickByXpath("//*[@class='closecross2']");
            Thread.Sleep(TimeSpan.FromSeconds(rd.Next(3, 10)));

            //2.点击新闻，进入新闻列表
            counter = 0;
            while (!result)
            {
                seleniumUtils.ClickByXpath("//*[@class='navi']/*[@class='container']//li[3]/div[@class='theme']");
                result = seleniumUtils.ClickByXpath("//*[@class='subtheme']/span/a[@href='/News/InstantNews']");
                counter++;
                if (counter > maxCounter) break;
            }
            Thread.Sleep(TimeSpan.FromSeconds(rd.Next(3, 10)));

            //menu失败的时候，通过内容进去
            counter = 0;
            while (!result)
            {
                result = seleniumUtils.ClickByXpath($"//*[@id='maincontent']//div[contains(@class,'instantnews')]//li[{rd.Next(1, 7)}]/a[@class='tdclass']");
                counter++;
                if (counter > maxCounter) break;
            }

            //3.查看新闻
            seleniumUtils.ClickByXpath($"//*[@class='news_list'][{rd.Next(1, 20)}]/a");
            Thread.Sleep(TimeSpan.FromSeconds(rd.Next(3, 10)));

            //4.循环查看新闻5次
            for (int i = 0; i < 5; i++)
            {
                seleniumUtils.ClickByXpath("//*[@class='breadcrumbnavigation']/span[1]/a");
                Thread.Sleep(TimeSpan.FromSeconds(rd.Next(3, 10)));

                seleniumUtils.ClickByXpath($"//*[@class='news_list'][{rd.Next(1, 20)}]/a");
                Thread.Sleep(TimeSpan.FromSeconds(rd.Next(10, 20)));
            }
        }

        /// <summary>
        /// 官网跳转
        /// </summary>
        private void RandomClickMGT()
        {
            bool result = false;

            seleniumUtils.SkipPage();
            Thread.Sleep(TimeSpan.FromSeconds(rd.Next(3, 10)));

            //1.关闭弹框
            seleniumUtils.ClickByXpath("//span[@class='closecross2']");
            Thread.Sleep(TimeSpan.FromSeconds(rd.Next(3, 10)));

            //2.点击新闻，进入新闻列表
            counter = 0;
            while (!result)
            {
                result = seleniumUtils.ClickByXpath("//a[@href='/News/InstantNewsList']/font");
                counter++;
                if (counter > maxCounter) break;
                Thread.Sleep(100);
            }
            Thread.Sleep(TimeSpan.FromSeconds(rd.Next(3, 10)));

            //3.查看新闻
            seleniumUtils.ClickByXpath($"//ul[@class='list-unstyled']//li[{rd.Next(1, 15)}]/a");
            Thread.Sleep(TimeSpan.FromSeconds(rd.Next(10, 20)));

            //4.循环查看新闻5次
            for (int i = 0; i < 5; i++)
            {
                seleniumUtils.ClickByXpath($"//ul[@class='list-unstyled']//li[{rd.Next(1, 15)}]/a");
                Thread.Sleep(TimeSpan.FromSeconds(rd.Next(10, 20)));
            }
        }

    }
}
