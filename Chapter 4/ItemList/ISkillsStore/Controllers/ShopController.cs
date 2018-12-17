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

        public ActionResult Index()
        {
            var data = db.Products.Select(p => p).OrderBy(p => p.ProductName);
            return View(data);
        }
	}
}