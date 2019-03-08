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
            return View();
        }
    }
}