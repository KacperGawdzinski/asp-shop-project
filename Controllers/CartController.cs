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
            var tab = Session["Cart"] as Dictionary<int,int>;
            if(tab == null) {
                tab = new Dictionary<int, int>();
            }
            using (var context = new DatabaseDataContext()) {
                var cart = new Cart() { cart = new Dictionary<Models.Processor, int>() };
                foreach (var v in tab) {
                    cart.cart.Add((from pr in context.Processor where pr.id == v.Key select pr).Select(x => new Models.Processor {
                        id = x.id,
                        name = x.name,
                        hash = x.hash,
                        price = (double)x.price
                    }).Single(),v.Value);
                }
                return View(cart);
            }
        }
        [HttpPost]
        public ActionResult Index(bool b)
        {
            var tab = Session["Cart"] as Dictionary<int, int>;

            using (var context = new DatabaseDataContext())
            {
                string products = "";
                string quantities = "";
                double sum = 0;
                foreach (var item in tab)
                {
                    products = products + item.Key + ",";
                    quantities = quantities + item.Value + ",";
                    sum += (double)(from v in context.Processor where v.id == item.Key select v).Single().price;
                }

                context.Order.Append(new Order() { price = sum, products = products, quantity = quantities, user_id = 0 /*TODO id zalogowanego */ });
                context.SubmitChanges();
            }
            return View(tab);
        }
    }
}