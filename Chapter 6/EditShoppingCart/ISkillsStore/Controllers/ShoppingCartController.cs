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

        public PartialViewResult CartWidget()
        {
            return PartialView(GetCart());
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

    }
}