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

        public ActionResult Index(int page = 1, int categoryID = 0, string searchString = "")
        {
            return View(GetModel(page, categoryID, searchString));
        }

        [HttpPost]
        public ActionResult Index(ProductsModel productsModel)
        {
            return View(GetModel(1, productsModel.CategoryID, productsModel.SearchString));
        }

        private ProductsModel GetModel(int page, int categoryID, string searchString)
        {
            var data = db.Products.Select(p => p)
                .Where(p => categoryID == 0 || p.CategoryID == categoryID)
                .Where(p => string.IsNullOrEmpty(searchString) || p.Description.Contains(searchString))
                        .OrderBy(p => p.ProductName)
                        .Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            ProductsModel model = new ProductsModel
            {
                Products = data,
                Pagination = new PaginationModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PAGE_SIZE,
                    TotalItems = categoryID == 0 ?
                        db.Products.Count() :
                        db.Products.Select(p => p)
                        .Where(p => p.CategoryID == categoryID)
                        .Where(p => p.Description.Contains(searchString)).Count()
                },
                CategoryID = categoryID
            };

            return model;
        }

    }
}