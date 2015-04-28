using Web.Messaging;

namespace Web.Messages
{
    public class MaterialQuantityChanged : Message
    {
        public string MaterialNumber { get; set; }
        public int Quantity { get; set; }
    }
}
