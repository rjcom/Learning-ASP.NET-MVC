using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISkillsStore.Models;

namespace ISkillsStore.Controllers
{
    public class ShopController : Controller
    {
        private ISkillsContext db = new ISkillsContext();
        private const int PAGE_SIZE = 3;

        public ActionResult Index(int page = 1)
        {
            var data = db.Products.Select(p => p)
                .OrderBy(p => p.ProductName)
                .Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            ProductsModel model = new ProductsModel
            {
                Products = data,
                Pagination = new PaginationModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PAGE_SIZE,
                    TotalItems = db.Products.ToList().Count()
                }
            };

            return View(model);
        }
    }
}