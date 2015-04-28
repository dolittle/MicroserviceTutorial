using Web.Messaging;

namespace Web.Messages
{
    public class OrderPickingStarted : Message
    {
        public string OrderNumber { get; set; }
    }
}
