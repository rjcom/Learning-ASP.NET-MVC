using System.Collections.Generic;

namespace ISkillsStore.Models
{
    public class ProductsModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PaginationModel Pagination { get; set; }
    }
}