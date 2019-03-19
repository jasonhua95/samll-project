using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Web2 value1", "Web2 value2" };
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetTest()
        {
            return new string[] { "test value1", "test value2" };
        }
    }
}
