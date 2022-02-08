using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost]
        public ActionResult Index()
        {
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
    }
}