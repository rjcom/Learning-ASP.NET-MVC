using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISkillsStore.Models;
using System.Web.Security;

namespace ISkillsStore.Controllers
{
    public class LoginController : Controller
    {
        private ISkillsContext db = new ISkillsContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Customer customer, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                int customerID;
                if (IsValid(customer.Email, customer.Password, out customerID))
                {
                    FormsAuthentication.SetAuthCookie(customerID.ToString(), false);

                    if (string.IsNullOrEmpty(returnUrl) || 
                        returnUrl.ToLower().Contains("login"))
                            returnUrl = Url.Action("Index", "Home");
                    return Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "The username and/or password is incorrect, please try again");
                }
            }
            return View(customer);
        }

        public bool IsValid(string Email, string Password, out int CustomerID)
        {
            string passwordHash = SHA256.Encode(Password);
            var data = from u in db.Customers
                       where u.Email == Email && u.Password == passwordHash
                       select new
                       {
                           u.CustomerID,
                           u.Email,
                           u.Password
                       };
            if (data.Count() > 0)
            {
                CustomerID = data.First().CustomerID;
                return true;
            }
            CustomerID = 0;
            return false;
        }


	}
}