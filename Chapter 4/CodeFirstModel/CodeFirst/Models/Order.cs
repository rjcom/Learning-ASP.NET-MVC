using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; }

        public virtual Customer Customer { get; set; }
    }
}