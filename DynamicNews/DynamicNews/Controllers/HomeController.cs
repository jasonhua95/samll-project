using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DynamicNews.Models;
using DynamicNews.Services;
using Microsoft.AspNetCore.Mvc;

namespace DynamicNews.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var newsName = NewsService.FindNewsName();
            var tag = NewsService.FindNewsTag(newsName.First());
            Dictionary<string, string> dic = tag.ToDictionary(x => x, x => $"ceshi{x} {DateTime.Now}");
            var filePath = NewsService.AutoDynamicNews(dic, newsName.First());
            ViewBag.filePath = filePath;
            return View();
        }
    }
}
