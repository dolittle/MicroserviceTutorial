using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string MaterialNumber { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}