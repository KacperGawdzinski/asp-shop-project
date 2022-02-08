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
        [HttpGet]
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
        public ActionResult Logged(int id)
        {
            if (Session["Cart"] == null)
            {
                Session["Cart"] = new Dictionary<int,int>();
            }

            using (var context = new DatabaseDataContext())
            {
                Processor p = (from a in context.Processor where a.id == id select a).Single();
                Models.Processor processor = new Models.Processor() { price = (float)p.price, hash= p.hash, id=p.id, name = p.name};
                int v=0;
                bool try_find = ((Dictionary<int, int>)Session["Cart"]).TryGetValue(id,out v);
                if (try_find) {
                    ((Dictionary<int, int>)Session["Cart"])[id]= v + 1;
                }
                else {
                    ((Dictionary<int, int>)Session["Cart"]).Add(id, 1);
                }
                return Logged();
            }
        }
 
        [HttpPost]
        public ActionResult Admin(int id)
        {
            using (var context = new DatabaseDataContext())
            {
                Processor p = (from a in context.Processor where a.id == id select a).Single();
                context.Processor.DeleteOnSubmit(p);
                context.SubmitChanges();
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
    }
}