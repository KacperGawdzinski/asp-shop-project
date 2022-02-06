using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models;

namespace project.Controllers {
    public class ShopController : Controller {
        project.Models.Processor[] procs = new project.Models.Processor[] {
            new project.Models.Processor(){ id = 0, name = "i7 8700k", hash="z1.jpg", price = 900},
            new project.Models.Processor(){ id = 1, name = "i7 10700k", hash="z2.jpg", price = 1400}
        };
        // GET: Shop
        public ActionResult Public() {
            return View(procs);
        }
        public ActionResult Logged() {
            return View(procs);
        }
        public ActionResult Admin() {
            return View(procs);
        }
    }
}