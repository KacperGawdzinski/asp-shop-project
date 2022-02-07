using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.Models;

namespace project.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var tab = new Models.Order[] {
                new Models.Order() { id=0, price=69.99f, products=new string[]{ "i5 5700", "i5 6900" }, quantities=new int[]{ 1, 2 }, user_id=0 },
                new Models.Order() { id=0, price=69.99f, products=new string[]{ "i5 5700", "i5 6900" }, quantities=new int[]{ 1, 2 }, user_id=0 },
                new Models.Order() { id=0, price=69.99f, products=new string[]{ "i5 5700", "i5 6900" }, quantities=new int[]{ 1, 2 }, user_id=0 },
                new Models.Order() { id=0, price=69.99f, products=new string[]{ "i5 5700", "i5 6900" }, quantities=new int[]{ 1, 2 }, user_id=0 },
                new Models.Order() { id=0, price=69.99f, products=new string[]{ "i5 5700", "i5 6900" }, quantities=new int[]{ 1, 2 }, user_id=0 },
            };

            return View(tab);
        }
    }
}