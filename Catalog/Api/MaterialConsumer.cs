using Web.Messages;
using Web.Messaging;
using Web.Models;
using Sample = Web.Messages.Sample;

namespace Web.Api
{
    public class MaterialConsumer : IMessageConsumer<MaterialAdded>
    {
        public void Handle(MaterialAdded message)
        {
            var productContext = new ProductDbContext();
            productContext.Insert(new Product()
            {
                Category = message.Category,
                Description = message.Description,
                Id = message.MaterialNumber,
                Price = message.Price,
                Title = message.Name
            });
        }
    }
}
