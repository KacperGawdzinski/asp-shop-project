using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(String s)
        {
            if (s == "Orders")
            {
                using (var context = new DatabaseDataContext())
                {
                    Models.Order[] procs = context.Order.Select(x => new Models.Order
                    {
                        id = x.id, 
                        products = x.products.Split(','), 
                        user_id = (int)x.user_id , 
                        price = (int)x.price, 
                        quantities = Array.ConvertAll(x.quantity.Split(','), d => int.Parse(d))
                    }).ToArray();
                    return View(procs);
                }
            }
            else
            {
                using (var context = new DatabaseDataContext())
                {
                    Models.User[] procs = context.User.Select(x => new Models.User
                    {
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
}