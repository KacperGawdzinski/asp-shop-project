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
            var tab = Session["Cart"] as List<Models.Order>;
            return View(tab);
        }
        public ActionResult Index(Models.Order o)
        {
            var tab = Session["Cart"] as List<Models.Order>;
            using (var context = new DatabaseDataContext())
            {
                string products = "";
                foreach (var item in o.products)
                {
                    products = products + item + ",";
                }
                string quantities = "";
                foreach (var item in o.quantities)
                {
                    quantities = quantities + item.ToString() + ",";
                }
                context.Order.Append(new Order() { id = o.id, price = o.price, products = products, quantity = quantities, user_id = 0 /*TODO id zalogowanego */ });
                context.SubmitChanges();
            }
            return View(tab);
        }
    }
}