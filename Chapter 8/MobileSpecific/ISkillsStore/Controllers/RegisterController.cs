using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISkillsStore.Models;

namespace ISkillsStore.Controllers
{
    public class RegisterController : Controller
    {
        private ISkillsContext db = new ISkillsContext();

        public ActionResult Index()
        {
            return View(new Customer());
        }

        public RedirectToRouteResult Register(Customer customer)
        {
            customer.Password = SHA256.Encode(customer.Password);

            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }


	}
}