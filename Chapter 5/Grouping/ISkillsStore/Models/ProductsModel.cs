using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace ISkillsStore.Models
{
    public class ProductsModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PaginationModel Pagination { get; set; }
        public int CategoryID { get; set; }

        public SelectList Categories()
        {
            ISkillsContext db = new ISkillsContext();
            var categories = from c in db.Categories
                             orderby c.CategoryName
                             select new
                             {
                                 c.CategoryID,
                                 c.CategoryName,
                             };
            return new SelectList(categories, "CategoryID", "CategoryName");
        }
    }
}