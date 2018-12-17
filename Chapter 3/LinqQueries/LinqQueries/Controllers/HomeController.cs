using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LinqQueries.Models;

namespace LinqQueries.Controllers
{
    public class HomeController : Controller
    {
        private NorthwindEntities db = new NorthwindEntities();

        public ActionResult Index()
        {
            // basic select with where clause
            //var products = db.Products.Where(p => p.UnitPrice > 50M).Select(p => p);

            // order by
            //var products = db.Products.OrderByDescending(p => p.UnitPrice);

            // take
            var products = db.Products.OrderByDescending(p => p.UnitPrice).Take(5);

            return View(products.ToList());
        }

        public ActionResult Categories()
        {
            // select distinct
            var categories = db.Products.Select(p => p.CategoryID).Distinct();

            // count
            ViewBag.CategoryCount = db.Products.Select(p => p.CategoryID).Distinct().Count();

            return View(categories.ToList());
        }

        public ActionResult Orders()
        {
            // join
            var orders = from c in db.Customers
                         join o in db.Orders
                         on c.CustomerID equals o.CustomerID
                         where o.Freight > 25M
                         orderby o.Freight descending
                         select new JoinedOrder()
                         { OrderID = o.OrderID,
                           CustomerID = o.CustomerID,
                           CompanyName = c.CompanyName,
                           ContactName = c.ContactName,
                           Freight = o.Freight,
                           ShipVia = o.ShipVia
                         };

            return View(orders.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
