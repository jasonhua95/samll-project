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
            return View();
        }
    }
}