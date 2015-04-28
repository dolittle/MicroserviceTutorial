using Web.Messaging;

namespace Web.Messages
{
    public class MaterialAdded : Message
    {
        public string MaterialNumber { get; set; }
        public string Vendor { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set;}
    }
}
