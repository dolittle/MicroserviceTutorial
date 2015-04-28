using Web.Messaging;

namespace Web.Messages
{
    public class ItemRemovedFromCart : Message
    {
        public string MaterialNumber { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
