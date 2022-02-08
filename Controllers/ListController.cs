using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace project.Controllers
{
    [Authorize(Roles = "admin")]
    public class ListController : Controller
    {
        // GET: List
        [HttpGet]
        public ActionResult Orders()
        {
            using (var context = new DatabaseDataContext()) {
                Models.Order[] procs = context.Order.Select(x => new Models.Order {
                    id = x.id,
                    products = x.products.Split(','),
                    user_id = (int)x.user_id,
                    price = (float)x.price,
                    quantities = Array.ConvertAll(x.quantity.Split(','),int.Parse)
                }).ToArray();
                foreach (var x in procs) {
                    List<string> tab = new List<string>();
                    foreach(var product in x.products) {
                        string s = (from p in context.Processor where p.id.ToString() == product select p.name).First();
                        tab.Add(s);
                    }
                    x.products = tab.ToArray();
                }
                return View(procs);
            }
        }
        [HttpGet]
        public ActionResult Users() {
            using (var context = new DatabaseDataContext()) {
                Models.User[] procs = context.User.Select(x => new Models.User {
                    id = x.id,
                    adress = x.adress,
                    password = x.passwd,
                    username = x.username
                }).ToArray();
                return View(procs);
            }
        }
    }
}