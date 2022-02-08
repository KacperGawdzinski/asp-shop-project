using System;
using System.Collections.Generic;
using System.IO;
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
        [HttpGet]
        public ActionResult AddProcessor(int? a) {
            return View();
        }
        [HttpPost]
        public ActionResult AddProcessor(object fileData) {
            HttpPostedFileBase file = null;
            if (Request.Files.Count > 0) {
                try {
                    HttpFileCollectionBase files = Request.Files;
                    for(int i = 0; i < files.Count; i++) {
                        file = files[i];
                        string fname;
                        fname = file.FileName;
                        fname = Path.Combine(Server.MapPath("~\\Content\\Images"), fname);
                        file.SaveAs(fname);
                    }
                }
                catch {

                }
                var name = Request.Params["name"];
                var price = Request.Params["price"];
                using (var context = new DatabaseDataContext()) {
                    context.Processor.InsertOnSubmit(new Processor() {id = context.Processor.OrderByDescending(x => x.id).First().id + 1, hash = file.FileName , name=name,price = Convert.ToDouble(price)});
                    context.SubmitChanges();
                }
            }
            return View();
        }
    }
}