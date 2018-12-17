using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISkillsStore.Models;

namespace ISkillsStore.Controllers
{
    public class ItemDetailController : Controller
    {
        private ISkillsContext db = new ISkillsContext();

        public ActionResult Index(int id)
        {
            var data = db.Products.SingleOrDefault(p => p.ProductID == id);
            return View(data);
        }
	}
}