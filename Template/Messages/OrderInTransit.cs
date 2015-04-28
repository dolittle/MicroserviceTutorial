using Web.Messaging;

namespace Web.Messages
{
    public class OrderInTransit : Message
    {
        public string OrderNumber { get; set; }
    }
}
