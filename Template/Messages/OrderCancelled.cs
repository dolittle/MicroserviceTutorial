using Web.Messaging;

namespace Web.Messages
{
    public class OrderCancelled : Message
    {
        public string OrderNumber { get; set; }
    }
}
