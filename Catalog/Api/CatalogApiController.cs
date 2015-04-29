using System;
using System.Collections.Generic;
using System.Web.Http;
using Web.Messages;
using Web.Messaging;
using Web.Models;
using Sample = Web.Messages.Sample;

namespace Web.Api
{
    public class CatalogApiController : ApiController
    {
        IMessageBroker _messageBroker;

        public CatalogApiController(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        [HttpGet]
        public List<Product> GetProductsByCategory(string category)
        {
            var dbContext = new ProductDbContext();
            return dbContext.GetByCategory(category);
        }

        [HttpGet]
        public string AddToCart(string productId, decimal price)
        {
            var name = "Add to cart : "+productId;
            _messageBroker.Send(new ItemAddedToCart
            {
                MaterialNumber = productId,
                Price = price,
                Quantity = 1
            });

            return "Added";
        }
    }
}
