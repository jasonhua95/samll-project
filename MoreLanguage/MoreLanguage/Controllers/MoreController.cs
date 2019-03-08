using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoreLanguage.Controllers
{
    public class MoreController : Controller
    {
        // GET: More
        public ActionResult Index(string lang)
        {
            ViewBag.lang = lang == "en" ? "Hello World!" :
                lang == "zh" ? "世界，你好！" : $"你的语言我不懂：{lang}";
            return View();
        }
    }
}