using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Web.Models;

namespace Web.Api
{
    public class CartController : ApiController
    {

        [HttpGet]
        public List<CartItem> Cart()
        {
            var dbContext = new CartDbContext();

            var cartItems = dbContext.GetAll();
            return cartItems;
        }

        [HttpPost]
        public void Remove(string id)
        {
            var dbContext = new CartDbContext();

             dbContext.RemoveItem(int.Parse(id));
        }
    }
}