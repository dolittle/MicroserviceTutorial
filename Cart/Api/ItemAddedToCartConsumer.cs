using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Messages;
using Web.Messaging;
using Web.Models;

namespace Web.Api
{
    public class ItemAddedToCartConsumer: IMessageConsumer<ItemAddedToCart>
        {
        public void Handle(ItemAddedToCart message)
            {
                var dbContext = new CartDbContext();

            var item = new CartItem()
            {
                MaterialNumber = message.MaterialNumber,
                Price = message.Price,
                Quantity = message.Quantity
            };

                dbContext.InsertItem(item);
            }
        }
    
}