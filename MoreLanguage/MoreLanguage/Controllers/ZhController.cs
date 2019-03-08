using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoreLanguage.Controllers
{
    public class ZhController : Controller
    {
        // GET: Zh
        public ActionResult Index()
        {
            ViewBag.testsession = HttpContext.Session.GetString("test");
            string testcookie;
            Request.Cookies.TryGetValue("testcookie", out testcookie);
            ViewBag.testcookie = testcookie;
            return View();
        }
    }
}