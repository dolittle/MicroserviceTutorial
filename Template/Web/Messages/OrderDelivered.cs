using Web.Messaging;

namespace Web.Messages
{
    public class OrderDelivered : Message
    {
        public string OrderNumber { get; set; }
    }
}
