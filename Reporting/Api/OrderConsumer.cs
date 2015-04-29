using Web.Messages;
using Web.Messaging;
using Web.Models;

namespace Web.Api
{
   /* public class OrderConsumer : IMessageConsumer<OrderPlaced>
    {
        public void Handle(OrderPlaced message)
        {
            var dbContext = new OrderDbContext();

            var order = dbContext.InsertOrUpdateOrder(message.OrderNumber,OrderStatus.PLACED);
        }

        
    }*/

    public class MessageConsumer : IMessageConsumer<Message>
    {
        public void Handle(Message message)
        {
            var dbContext = new OrderDbContext();
            if (message is OrderPlaced)
            {
                var opMessage = message as OrderPlaced;
                var order = dbContext.InsertOrUpdateOrder(opMessage.OrderNumber, OrderStatus.PLACED);
            }
            else if (message is OrderPickingStarted)
            {
                var opMessage = message as OrderPickingStarted;
                var order = dbContext.InsertOrUpdateOrder(opMessage.OrderNumber, OrderStatus.PICKING_STARTED);
            }
            else if (message is OrderPicked)
            {
                var opMessage = message as OrderPicked;
                var order = dbContext.InsertOrUpdateOrder(opMessage.OrderNumber, OrderStatus.PICKED);
            }
            else if (message is OrderCancelled)
            {
                var opMessage = message as OrderCancelled;
                var order = dbContext.InsertOrUpdateOrder(opMessage.OrderNumber, OrderStatus.CANCELED);
            }
            else if (message is OrderPackaged)
            {
                var opMessage = message as OrderPackaged;
                var order = dbContext.InsertOrUpdateOrder(opMessage.OrderNumber, OrderStatus.PACKAGED);
            }
            else if (message is OrderInTransit)
            {
                var opMessage = message as OrderInTransit;
                var order = dbContext.InsertOrUpdateOrder(opMessage.OrderNumber, OrderStatus.IN_TRANSIT);
            }
            else if (message is OrderDelivered)
            {
                var opMessage = message as OrderDelivered;
                var order = dbContext.InsertOrUpdateOrder(opMessage.OrderNumber, OrderStatus.DELIVERED);
            }
        }


    }
}
