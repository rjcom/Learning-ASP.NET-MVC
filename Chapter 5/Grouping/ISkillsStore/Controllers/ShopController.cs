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

        public ActionResult Index(int page = 1, int categoryID = 0)
        {
            return View(GetModel(page, categoryID));
        }

        [HttpPost]
        public ActionResult Index(ProductsModel productsModel)
        {
            int page = 1;
            int categoryID = productsModel.CategoryID;
            return View(GetModel(page, categoryID));
        }

        private ProductsModel GetModel(int page, int categoryID)
        {
            var data = db.Products.Select(p => p).Where(p => categoryID == 0 || p.CategoryID == categoryID)
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
                        db.Products.Select(p => p).Where(p => p.CategoryID == categoryID).Count()
                },
                CategoryID = categoryID
            };

            return model;
        }

    }
}