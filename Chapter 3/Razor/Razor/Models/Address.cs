using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Razor.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        public string Street { get; set; }
        public string City { set; get; }
        public string State { set; get; }
        public string PostalCode { set; get; }
    }
}