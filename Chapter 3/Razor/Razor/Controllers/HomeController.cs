using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Razor.Models;

namespace Razor.Controllers
{
    public class HomeController : Controller
    {
        Customer demoCustomer = new Customer
        {
            CustomerID = 1,
            ContactName = "Gregory Marley",
            CompanyName = "Brothers and Sisters",
            Address = new Address
            {
                AddressID = 1,
                Street = "25 South St.",
                City = "Jacksonville",
                State = "Florida",
                PostalCode = "32205"
            }
        };

        public ActionResult Index()
        {
            return View(demoCustomer);
        }

        public ActionResult Address()
        {
            return View(demoCustomer.Address);
        }
    }
}