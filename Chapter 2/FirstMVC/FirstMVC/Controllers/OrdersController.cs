using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FirstMVC.Models;

namespace FirstMVC.Controllers
{
    public class OrdersController : Controller
    {
        //
        // GET: /Orders/
        public ActionResult Index()
        {
            // create demo model data
            List<OrderModel> orders = GetOrderDemoModels();

            // we'll put the orders and customers in the session - this is not the norm
            // normally, there is a data context hooked to a database - we'll get there :-)
            Session["Orders"] = orders;
            Session["Customers"] = ViewBag.Customers;

            // send the model to the view
            return View(orders);
        }

        private List<CustomerModel> GetCustomerDemoModels()
        {
            List<CustomerModel> customers = new List<CustomerModel>{
                new CustomerModel{
                    CustomerID=1,
                    FirstName="Bob",
                    LastName="Plant",
                    Address="25 Mountain Way",
                    City="Franconia",
                    State="NH",
                    PostalCode="03555",
                    PhoneNumber="603 555-1212"
                },
                new CustomerModel{
                    CustomerID=2,
                    FirstName="Joe",
                    LastName="Perry",
                    Address="5 Main st.",
                    City="Boston",
                    State="MA",
                    PostalCode="02555",
                    PhoneNumber="617 555-1212"
                },
                new CustomerModel{
                    CustomerID=3,
                    FirstName="Christine",
                    LastName="Hide",
                    Address="85 Madison ave.",
                    City="Akron",
                    State="OH",
                    PostalCode="45205",
                    PhoneNumber="513 555-1212"
                }
            };
            return customers;
        }

        private List<OrderModel> GetOrderDemoModels()
        {
            List<OrderModel> orders = new List<OrderModel>
            {
                new OrderModel{
                    OrderID=1,
                    CustomerID=2,
                    Item="Gibson LesPaul",
                    Price=2025.00M,
                    Quantity=1
                },
                new OrderModel{
                    OrderID=2,
                    CustomerID=2,
                    Item="Marshall Amplifier",
                    Price=2525.00M,
                    Quantity=2
                },
                new OrderModel{
                    OrderID=3,
                    CustomerID=1,
                    Item="Neumann U67 Microphone",
                    Price=1025.00M,
                    Quantity=1
                },
                new OrderModel{
                    OrderID=4,
                    CustomerID=3,
                    Item="Fender Blackface Amplifier",
                    Price=2542.00M,
                    Quantity=1
                },
                new OrderModel{
                    OrderID=5,
                    CustomerID=3,
                    Item="Fender Telecaster",
                    Price=1025.00M,
                    Quantity=1
                }
            };

            return orders;
        }

        private void SetupCustomersList(List<CustomerModel> Customers)
        {
            IEnumerable<SelectListItem> items = Customers.Select(customer => new SelectListItem
            {
                Value = customer.CustomerID.ToString(),
                Text = customer.FirstName + " " + customer.LastName
            });
            ViewBag.Customers = items;
        }

        public ActionResult Edit(int? id)
        {
            List<OrderModel> orders = (List<OrderModel>)Session["Orders"];
            OrderModel order = orders.Single(o => o.OrderID == id);
            if (order == null)
            {
                return HttpNotFound();
            }

            // load customer list items
            List<CustomerModel> customers = GetCustomerDemoModels();
            SetupCustomersList(customers);

            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(OrderModel order)
        {
            // here's where you would save the edited data
            return RedirectToAction("Index");
        }

	}
}