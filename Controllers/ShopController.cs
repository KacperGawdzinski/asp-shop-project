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
        [Authorize]
        public ActionResult Logged() {
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
        [Authorize(Roles = "admin")]
        public ActionResult Admin() {
            using (var context = new DatabaseDataContext())
            {
                project.Models.Processor[] procs = context.Processor.Select(x => new Models.Processor
                {
                    id = x.id,
                    name = x.name,
                    hash = x.hash,
                    price = (double)x.price
                }).ToArray();
                return View(procs);
            }   
        }
        [HttpPost]
        [Authorize]
        public ActionResult Logged(Models.Processor p)
        {
            if (Session["Cart"] == null)
            {
                Session["Cart"] = new List<Models.Order>();
            }
            Models.Order order = new Models.Order() { price = (float)p.price, products = new string[] { p.name }, quantities = new int[] { 1 }, user_id = 0 /* TODO id zalogowanego*/ };
            ((List<Models.Order>)Session["Cart"]).Add(order);
            using (var context = new DatabaseDataContext())
            {
                Models.Processor[] procs = context.Processor.Select(x => new Models.Processor
                {
                    id = x.id,
                    name = x.name,
                    hash = x.hash,
                    price = (double)x.price
                }).ToArray();
                return View(procs);
            }
        }
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public ActionResult Admin(Models.Processor o)
        {
            using (var context = new DatabaseDataContext())
            {
                context.Processor.DeleteOnSubmit(new Processor() { id = o.id, name = o.name, hash = o.hash, price=o.price});
                context.SubmitChanges();
            }
            using (var context = new DatabaseDataContext())
            {
                Models.Processor[] procs = context.Processor.Select(x => new Models.Processor
                {
                    id = x.id,
                    name = x.name,
                    hash = x.hash,
                    price = (double)x.price
                }).ToArray();
                return View(procs);
            }
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Admin(Models.Processor o, int n) // TODO dodawanie elementu funkcja
        {
            using (var context = new DatabaseDataContext())
            {
                context.Processor.Append(new Processor() { id = o.id, name = o.name, hash = o.hash, price = o.price });
                context.SubmitChanges();
            }
            using (var context = new DatabaseDataContext())
            {
                Models.Processor[] procs = context.Processor.Select(x => new Models.Processor
                {
                    id = x.id,
                    name = x.name,
                    hash = x.hash,
                    price = (double)x.price
                }).ToArray();
                return View(procs);
            }
        }
    }
}