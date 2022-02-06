using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models;

namespace project.Controllers {
    public class ShopController : Controller {
        // GET: Shop
        public ActionResult Public() {
            //ViewBag.Login = "chuj";
            using (var context = new DatabaseDataContext()) {
               project.Models.Processor[] procs = context.Processor.Select(x => new Models.Processor {
                    id = x.id,
                    name = x.name,
                    hash = x.hash,
                    price = (double)x.price
                }).ToArray();
                return View(procs);
            }
            
        }
        public ActionResult Logged() {
            return View();
        }
        public ActionResult Admin() {
            return View();
        }
    }
}