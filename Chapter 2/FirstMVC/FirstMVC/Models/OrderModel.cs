using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstMVC.Models
{
    public class OrderModel
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int Quantity { get; set; }
        public string Item { get; set; }
        public decimal Price { get; set; }

    }
}