using Web.Messaging;

namespace Web.Messages
{
    public class MaterialPriceChanged : Message
    {
        public string MaterialNumber { get; set; }
        public decimal Price { get; set; }
    }
}
