using Web.Messaging;

namespace Web.Messages
{
    public class OrderPicked : Message
    {
        public string OrderNumber { get; set; }
    }
}
