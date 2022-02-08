using System.Web.Mvc;
using System;
using System.Web.Security;
using System.Web;
using System.Linq;

namespace project.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost]
        public ActionResult Index(string name, string passwd)
        {
            ShopMembershipProvider prov = new ShopMembershipProvider();
            using (var context = new DatabaseDataContext())
            {
                if (context.User.Where(x => x.username == name).Count() == 0) //brak uzytkownika o danym nicku
                    prov.CreateShopUser(name, passwd, String.Empty);
            }
            if (!prov.ValidateUser(name, passwd))
                return RedirectToAction("Shop", "Public");
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, name, DateTime.Now, DateTime.Now.AddMinutes(10), false, "");
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            Response.AppendCookie(cookie);
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
        [HttpPost]
        public ActionResult Logout(bool val)
        {
            FormsAuthentication.SignOut();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
        }
}