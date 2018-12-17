using System.Collections.Generic;
using System.Linq;

namespace ISkillsStore.Models
{

    public class ShoppingCartModel
    {
        private List<ShoppingCartItemModel> items = new List<ShoppingCartItemModel>();

        public IEnumerable<ShoppingCartItemModel> Items
        {
            get { return items; }
        }

        public void AddItem(Product product, int quantity)
        {
            ShoppingCartItemModel item = 
                items.SingleOrDefault(p => p.Product.ProductID == product.ProductID);

            if (item == null)
            {
                items.Add(new ShoppingCartItemModel
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public void RemoveItem(int productID)
        {
            items.RemoveAll(l => l.Product.ProductID == productID);
        }

        public decimal GetCartTotal()
        {
            return items.Sum(e => e.Product.Price * e.Quantity);

        }
        public void Clear()
        {
            items.Clear();
        }

    }

}
