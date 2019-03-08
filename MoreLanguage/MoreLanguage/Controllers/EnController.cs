using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoreLanguage.Controllers
{
    public class EnController : Controller
    {
        // GET: En
        public ActionResult Index()
        {
            HttpContext.Session.SetString("test", "session添加成功");
            Response.Cookies.Append("testcookie", "cookie添加成功");
            return View();
        }
    }
}