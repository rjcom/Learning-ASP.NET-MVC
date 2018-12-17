using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinqQueries.Models
{
    public class JoinedOrder
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public Nullable<int> ShipVia { get; set; }
        public Nullable<decimal> Freight { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
    }

}