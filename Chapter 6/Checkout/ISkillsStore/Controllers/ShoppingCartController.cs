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

        public ViewResult ShippingInfo()
        {
            return View(new ShippingInfo());
        }

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

        public ViewResult BillingInfo()
        {
            return View(new BillingInfo());
        }

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
            // to do: we need a login for our customer,
            // until then, we'll create one every time
            Customer customer = new Customer
            {
                FirstName = cart.BillingInfo.FirstName,
                LastName = cart.BillingInfo.LastName,
                BillingAddress = cart.BillingInfo.Address,
                BillingCity = cart.BillingInfo.City,
                BillingState = cart.BillingInfo.State,
                BillingPostalCode = cart.BillingInfo.Zip,
                CardNumber = cart.BillingInfo.CreditCardNumber,
                ExpiratonMonth = cart.BillingInfo.ExpirationMonth,
                ExpirationYear = cart.BillingInfo.ExpirationYear
            };
            db.Customers.Add(customer);
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