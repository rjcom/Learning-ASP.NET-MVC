﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Razor.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string ContactName { get; set; }
        public string CompanyName { get; set; }
        public Address Address { get; set; }
    }
}