using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISkillsStore.Models;

namespace ISkillsStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ISkillsContext db = new ISkillsContext();

        public ShoppingCartController()
        {
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new ShoppingCartViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(int productID, string returnUrl)
        {
            Product product = db.Products.SingleOrDefault(p => p.ProductID == productID);

            if (product != null)
            {
                GetCart().AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        {
            GetCart().RemoveItem(productId);
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult CartWidget(ShoppingCartModel cart)
        {
            return PartialView(cart);
        }

        private ShoppingCartModel GetCart()
        {
            ShoppingCartModel cart = (ShoppingCartModel)Session["Cart"];
            if (cart == null)
            {
                cart = new ShoppingCartModel();
                Session["Cart"] = cart;
            }
            return cart;
        }

        [Authorize]
        public ViewResult ShippingInfo()
        {
            return View(new ShippingInfo());
        }

        [Authorize]
        [HttpPost]
        public ActionResult ShippingInfo(ShippingInfo shippingInfo)
        {
            if (ModelState.IsValid)
            {
                ShoppingCartModel cart = GetCart();
                cart.ShippingInfo = shippingInfo;
                return RedirectToAction("BillingInfo");
            }
            else
            {
                return View(shippingInfo);
            }
        }

        [Authorize]
        public ViewResult BillingInfo()
        {
            return View(new BillingInfo());
        }

        [Authorize]
        [HttpPost]
        public ViewResult BillingInfo(BillingInfo billingInfo)
        {
            if (ModelState.IsValid)
            {
                ShoppingCartModel cart = GetCart();
                cart.BillingInfo = billingInfo;
                ProcessOrder(cart);
                cart.Clear();
                return View("OrderComplete");
            }
            else
            {
                return View(billingInfo);
            }
        }

        private void ProcessOrder(ShoppingCartModel cart)
        {
            string idString = System.Web.HttpContext.Current.User.Identity.Name;
            int customerID = int.Parse(idString);

            Customer customer = db.Customers.SingleOrDefault(c => c.CustomerID == customerID);
            customer.FirstName = cart.BillingInfo.FirstName;
            customer.LastName = cart.BillingInfo.LastName;
            customer.BillingAddress = cart.BillingInfo.Address;
            customer.BillingCity = cart.BillingInfo.City;
            customer.BillingState = cart.BillingInfo.State;
            customer.BillingPostalCode = cart.BillingInfo.Zip;
            customer.CardNumber = cart.BillingInfo.CreditCardNumber;
            customer.ExpiratonMonth = cart.BillingInfo.ExpirationMonth;
            customer.ExpirationYear = cart.BillingInfo.ExpirationYear;

            db.SaveChanges();

            Order order = new Order
            {
                CustomerID = customer.CustomerID,
                OrderDate = DateTime.Now,
                ShippingAddress = cart.ShippingInfo.Address,
                ShippingCity = cart.ShippingInfo.City,
                ShippingState = cart.ShippingInfo.State,
                ShippingPostalCode = cart.ShippingInfo.Zip
            };
            db.Orders.Add(order);
            db.SaveChanges();

            foreach (ShoppingCartItemModel item in cart.Items)
            {
                OrderItem orderItem = new OrderItem
                {
                    OrderID = order.OrderID,
                    ProductID = item.Product.ProductID,
                    Quantity = item.Quantity
                };
                db.OrderItems.Add(orderItem);
            }
            db.SaveChanges();
        }
    }
}